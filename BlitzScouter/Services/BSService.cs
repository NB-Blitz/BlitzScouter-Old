using BlitzScouter.Models;
using BlitzScouter.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BlitzScouter.Services
{
    public class BSService
    {
        readonly BSRepo repo;

        public BSService(BSContext context)
        {
            repo = new BSRepo(context);
        }

        // Upload BSRaw
        public void addUserData(BSRaw model)
        {
            model.toStr();
            if (repo.containsTeam(model.team))
            {
                repo.addRound(model);
            }
            else
            {
                var team = new BSTeam()
                {
                    team = model.team
                };
                repo.addTeam(team);
                repo.addRound(model);
            }
        }

        // Update BSRaw
        public void setRound(BSRaw model)
        {
            if (model == null)
                return;
            model.toStr();
            if (repo.containsRound(model))
            {
                repo.updateRound(model);
            }
            else
            {
                repo.addRound(model);
            }
        }

        public void setMatch(BSMatch match)
        {
            if (match == null)
                return;
            if (repo.containsMatch(match.match))
            {
                BSMatch rMatch = repo.getMatch(match.match);
                rMatch.comments = match.comments;
                repo.saveData();
            }
            else
            {
                repo.addMatch(match);
            }
        }

        // Upload BSTeam
        public void setTeam(BSTeam team)
        {
            if (team == null)
                return;
            if (repo.containsTeam(team.team))
            {
                BSTeam rTeam = repo.getTeam(team.team);
                rTeam.name = team.name;
                rTeam.pitComments = team.pitComments;
                repo.saveData();
            }
            else
            {
                repo.addTeam(team);
            }
        }

        public List<String> getUpcomingRounds()
        {
            String tba = repo.getTBA("team/frc" + BSConfig.c.teamnum + "/event/" + BSConfig.c.tbaComp + "/matches");
            List<RootMatch> json = JsonConvert.DeserializeObject<List<RootMatch>>(tba);

            List<String> str = new List<String>();
            int startIndex = 0;
            foreach (RootMatch m in json)
            {
                String key = m.key.Substring(m.key.LastIndexOf("_") + 1);
                // Qualification Match
                if (m.comp_level == "qm")
                {
                    if (int.Parse(key.Substring(2)) < 10)
                    {
                        str.Insert(startIndex, "<a href=\"/Dash/Round?roundnum=" + key.Substring(2) + "\" class=\"nav-link text-dark sideLink matchLink\">Quals " + key.Substring(2) + "</a>");
                        startIndex++;
                    }
                    else
                    {
                        str.Add("<a href=\"/Dash/Round?roundnum=" + key.Substring(2) + "\" class=\"nav-link text-dark sideLink matchLink\">Quals " + key.Substring(2) + "</a>");
                    }
                }
            }
            foreach(RootMatch m in json)
            {
                String key = m.key.Substring(m.key.LastIndexOf("_") + 1);
                // Quarter Final
                if (m.comp_level == "qf")
                {
                    str.Add("<a href=\"/Dash/Round?raw=qf" + key.Substring(2, 1) + "m" + key.Substring(4) + "\" class=\"nav-link text-dark sideLink matchLink\">Quarters " + key.Substring(2,1) + " Match " + key.Substring(4) + "</a>");
                }
                // Semi Final
                else if (m.comp_level == "sf")
                {
                    str.Add("<a href=\"/Dash/Round?raw=sf" + key.Substring(2, 1) + "m" + key.Substring(4) + "\" class=\"nav-link text-dark sideLink matchLink\">Semis " + key.Substring(2,1) + " Match " + key.Substring(4) + "</a>");
                }
                // Final
                else if (m.comp_level == "f")
                {
                    str.Add("<a href=\"/Dash/Round?raw=f1m" + key.Substring(3) + "\" class=\"nav-link text-dark sideLink matchLink\">Finals " + key.Substring(3) + "</a>");
                }
            }

            return str;
        }

        // Get BSTeam
        public BSTeam getTeam(int team)
        {
            BSTeam tm;

            // Check if Exists
            if (repo.containsTeam(team))
                tm = repo.getTeam(team);
            else
            {
                tm = new BSTeam()
                {
                    team = team
                };
                repo.addTeam(tm);
            }
            tm.rounds = getRounds(team);

            // Calculate Averages
            tm.checkboxAverages = new List<double>();
            tm.counterAverages = new List<double>();
            for (int i = 0; i < tm.rounds.Count; i++)
            {
                // Checkbox
                for (int o = 0; o < tm.rounds[i].checkboxes.Count; o++)
                {
                    if (i == 0)
                    {
                        tm.checkboxAverages.Add(Convert.ToInt32(tm.rounds[i].checkboxes[o]));
                    }
                    else
                    {
                        tm.checkboxAverages[o] += Convert.ToInt32(tm.rounds[i].checkboxes[o]);
                    }
                }

                // Counter
                for (int o = 0; o < tm.rounds[i].counters.Count; o++)
                {
                    if (i == 0)
                    {
                        tm.counterAverages.Add(tm.rounds[i].counters[o]);
                    }
                    else
                    {
                        tm.counterAverages[o] += tm.rounds[i].counters[o];
                    }
                }
            }

            // Divide for Averages
            for (int i = 0; i < tm.checkboxAverages.Count; i++)
            {
                tm.checkboxAverages[i] /= tm.rounds.Count;
                // Fix Decimals
                tm.checkboxAverages[i] = Math.Round(tm.checkboxAverages[i] * 100) / 100;
            }
            for (int i = 0; i < tm.counterAverages.Count; i++)
            {
                tm.counterAverages[i] /= tm.rounds.Count;
                // Fix Decimals
                tm.counterAverages[i] = Math.Round(tm.counterAverages[i]);
            }

            return tm;
        }

        public List<BSTeam> getTopTeams()
        {
            List<BSTeam> top = getAllTeams();

            int sorted = 0;
            while (sorted < top.Count)
            {
                // Find Smallest
                int smallIndex = sorted;
                int score = int.MaxValue;
                for (int i = sorted; i < top.Count; i++)
                {
                    if (getScore(top[i]) < score)
                    {
                        smallIndex = i;
                    }
                }
                
                while (smallIndex > sorted)
                {
                    if (getScore(top[smallIndex - 1]) >= getScore(top[smallIndex]))
                    {
                        BSTeam temp = top[smallIndex];
                        top[smallIndex] = top[smallIndex - 1];
                        top[smallIndex - 1] = temp;
                        smallIndex--;
                    }
                    else
                    {
                        break;
                    }
                }
                sorted++;
            }

            return top;
        }

        private double getScore(BSTeam team)
        {
            if (team == null)
                return 0;
            if (team.counterAverages == null)
                return 0;

            double score = 0;
            for (int i = 0; i < team.counterAverages.Count; i++)
            {
                score += team.counterAverages[i];
            }
            return score;
        }

        // Get all Teams
        public List<BSTeam> getAllTeams()
        {
            
            // The Blue Alliance
            String tba = repo.getTBA("event/" + BSConfig.c.tbaComp + "/teams");
            List<RootTeam> json = JsonConvert.DeserializeObject<List<RootTeam>>(tba);

            List<BSTeam> teams = new List<BSTeam>();
            foreach (RootTeam tm in json)
            {
                teams.Add(getTeam(tm.teamNum));
            }
            

            /*
            // Internal
            List<BSTeam> teams = repo.GetAllTeams();
            for (int b = 0; b < teams.Count; b++)
            {
                BSTeam tm = teams[b];
                tm.rounds = getRounds(tm.team);

                // Calculate Averages
                tm.checkboxAverages = new List<double>();
                tm.counterAverages = new List<double>();
                for (int i = 0; i < tm.rounds.Count; i++)
                {
                    // Checkbox
                    for (int o = 0; o < tm.rounds[i].checkboxes.Count; o++)
                    {
                        if (i == 0)
                        {
                            tm.checkboxAverages.Add(Convert.ToInt32(tm.rounds[i].checkboxes[o]));
                        }
                        else
                        {
                            tm.checkboxAverages[o] += Convert.ToInt32(tm.rounds[i].checkboxes[o]);
                        }
                    }

                    // Counter
                    for (int o = 0; o < tm.rounds[i].counters.Count; o++)
                    {
                        if (i == 0)
                        {
                            tm.counterAverages.Add(tm.rounds[i].counters[o]);
                        }
                        else
                        {
                            tm.counterAverages[o] += tm.rounds[i].counters[o];
                        }
                    }
                }

                // Divide for Averages
                for (int i = 0; i < tm.checkboxAverages.Count; i++)
                {
                    tm.checkboxAverages[i] /= tm.rounds.Count;
                    // Fix Decimals
                    tm.checkboxAverages[i] = Math.Round(tm.checkboxAverages[i] * 100) / 100;
                }
                for (int i = 0; i < tm.counterAverages.Count; i++)
                {
                    tm.counterAverages[i] /= tm.rounds.Count;
                    // Fix Decimals
                    tm.counterAverages[i] = Math.Round(tm.counterAverages[i]);
                }
            }
            */
            

            return teams;
        }

        // Get All Rounds
        public List<BSRaw> getAllRounds()
        {
            List<BSRaw> arr = repo.getAll();
            foreach (BSRaw round in arr)
                round.toObj();
            return arr;
        }

        // Contains Team
        public bool containsTeam(int team)
        {
            return repo.containsTeam(team);
        }

        // Get all BSRaws
        public List<BSRaw> getRounds(int team)
        {
            List<BSRaw> arr = repo.getRounds(team);
            for (int i = 0; i < arr.Count; i++)
                arr[i].toObj();
            return arr;
        }

        public BSRaw getById(int id)
        {
            BSRaw raw = repo.getById(id);
            if (raw != null)
                raw.toObj();
            return raw;
        }

        // Get BSMatch
        public BSMatch getMatch(String match)
        {
            String tba = repo.getTBA("event/" + BSConfig.c.tbaComp + "/matches");
            List<RootObject> json = JsonConvert.DeserializeObject<List<RootObject>>(tba);

            // Check Data
            if (json.Count < 1 || match == null || match == "")
                return null;

            BSMatch bsmatch = new BSMatch()
            {
                match = match
            };

            if (repo.getMatch(match) != null)
                bsmatch.comments = repo.getMatch(match).comments;

            if (match.Substring(0, 2) == "qm")
            {
                bsmatch.matchStr = "Quals " + match.Substring(2);
            }
            else if (match.Substring(0, 2) == "qf")
            {
                bsmatch.matchStr = "Quarters " + match.Substring(2,1) + " Match " + match.Substring(4);
            }
            else if (match.Substring(0, 2) == "sf")
            {
                bsmatch.matchStr = "Semis " + match.Substring(2,1) + " Match " + match.Substring(4);
            }
            else if (match.Substring(0, 1) == "f")
            {
                bsmatch.matchStr = "Finals Match " + match.Substring(3);
            }
            System.Diagnostics.Debug.WriteLine(BSConfig.c.tbaComp + "_" + match);
            foreach (RootObject obj in json)
            {
                if (obj.key == BSConfig.c.tbaComp + "_" + match)
                {
                    bsmatch.blue = new List<BSTeam>();
                    for(int i = 0; i < obj.alliances.blue.team_keys.Count; i++)
                    {
                        int teamNum = int.Parse(obj.alliances.blue.team_keys[i].Substring(3));
                        bsmatch.blue.Add(getTeam(teamNum));
                    }

                    bsmatch.red = new List<BSTeam>();
                    for (int i = 0; i < obj.alliances.red.team_keys.Count; i++)
                    {
                        int teamNum = int.Parse(obj.alliances.red.team_keys[i].Substring(3));
                        bsmatch.red.Add(getTeam(teamNum));
                    }
                    return bsmatch;
                }
            }
            return null;
        }

        public void deleteRound(int id)
        {
            if (repo.getById(id) == null)
                return;
            repo.deleteRound(id);
        }
    }

    public class RootTeam
    {
        [JsonProperty("team_number")]
        public int teamNum { get; set; }
    }

    public class RootAlliance
    {
        [JsonProperty("picks")]
        List<string> picks { get; set; }

        [JsonProperty("status")]
        Status status { get; set; }
    }

    public class Status
    {
        [JsonProperty("status")]
        String status { get; set; }
    }

    // JSON Conversion
    public class RootObject
    {
        [JsonProperty("alliances")]
        public Alliances alliances { get; set; }

        [JsonProperty("key")]
        public String key { get; set; }

        [JsonProperty("winning_alliance")]
        public string winning_alliance { get; set; }
    }
    public class Alliances
    {
        [JsonProperty("blue")]
        public Blue blue { get; set; }

        [JsonProperty("red")]
        public Red red { get; set; }
    }
    public class Blue
    {
        [JsonProperty("score")]
        public int score { get; set; }

        [JsonProperty("team_keys")]
        public List<string> team_keys { get; set; }
    }
    public class Red
    {
        [JsonProperty("score")]
        public int score { get; set; }

        [JsonProperty("team_keys")]
        public List<string> team_keys { get; set; }
    }
    public class RootMatch
    {
        [JsonProperty("comp_level")]
        public String comp_level { get; set; }

        [JsonProperty("key")]
        public String key { get; set; }
    }
}
