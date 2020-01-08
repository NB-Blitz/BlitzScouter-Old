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
        BSRepo repo;

        public BSService(BSContext context)
        {
            repo = new BSRepo(context);
        }

        // Upload BSRaw
        public void addUserData(BSRaw model)
        {
            if (repo.containsTeam(model.team))
            {
                repo.addRound(model);
            }
            else
            {
                BSTeam team = new BSTeam();
                team.team = model.team;
                repo.addTeam(team);
                repo.addRound(model);
            }
        }

        // Upload BSTeam
        public void setTeam(BSTeam team)
        {
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

        // Get BSTeam
        public BSTeam getTeam(int team)
        {
            BSTeam tm;

            // Check if Exists
            if (repo.containsTeam(team))
                tm = repo.getTeam(team);
            else
            {
                tm = new BSTeam();
                tm.team = team;
                repo.addTeam(tm);
            }
            tm.rounds = getRounds(team);

            // Calculate Averages
            tm.averages = new List<double>();
            for (int i = 0; i < tm.rounds.Count; i++)
            {
                // Checkbox
                for (int o = 0; o < tm.rounds[i].checkboxes.Length; o++)
                {
                    if (i == 0)
                    {
                        tm.averages.Add(Convert.ToInt32(tm.rounds[i].checkboxes[o]));
                    }
                    else
                    {
                        tm.averages[o] += Convert.ToInt32(tm.rounds[i].checkboxes[o]);
                    }
                }

                // Counter
                for (int o = 0; o < tm.rounds[i].counters.Length; o++)
                {
                    if (i == 0)
                    {
                        tm.averages.Add(tm.rounds[i].counters[o]);
                    }
                    else
                    {
                        tm.averages[tm.rounds[i].checkboxes.Length + o] += tm.rounds[i].counters[o];
                    }
                }
            }

            // Divide for Averages
            for (int i = 0; i < tm.averages.Count; i++)
            {
                tm.averages[i] /= tm.rounds.Count;
                // Fix Decimals
                tm.averages[i] = Math.Round(tm.averages[i] * 100) / 100;
            }

            return tm;
        }

        // Get all BSRaws
        public List<BSRaw> getRounds(int team)
        {
            List<BSRaw> arr = repo.getRounds(team);
            for (int i = 0; i < arr.Count; i++)
                arr[i].toObj();
            return arr;
        }

        // Get BSMatch
        public BSMatch getMatch(int match)
        {
            String tba = repo.getTBA("event/" + BSConfig.tbaComp + "/matches");
            List<RootObject> json = JsonConvert.DeserializeObject<List<RootObject>>(tba);

            // Check Data
            if (json.Count < 1)
                return null;
            if (match < 1)
                return null;

            BSMatch bsmatch = new BSMatch();
            foreach(RootObject obj in json)
            {
                if (obj.match_number == match)
                {
                    bsmatch.blue = new List<BSTeam>();
                    for(int i = 0; i < obj.alliances.blue.team_keys.Count; i++)
                    {
                        int teamNum = int.Parse(obj.alliances.blue.team_keys[i].Substring(3));
                        System.Diagnostics.Debug.WriteLine("TBA Data: '" + obj.alliances.blue.team_keys[i] + "' '" + obj.alliances.blue.team_keys[i].Substring(3) + "'");
                        bsmatch.blue.Add(getTeam(teamNum));
                    }

                    bsmatch.red = new List<BSTeam>();
                    for (int i = 0; i < obj.alliances.red.team_keys.Count; i++)
                    {
                        int teamNum = int.Parse(obj.alliances.red.team_keys[i].Substring(3));
                        System.Diagnostics.Debug.WriteLine("TBA Data: '" + obj.alliances.red.team_keys[i] + "' '" + obj.alliances.red.team_keys[i].Substring(3) + "'");
                        bsmatch.red.Add(getTeam(teamNum));
                    }
                    return bsmatch;
                }
            }
            return null;
        }
    }

    // JSON Conversion
    public class RootObject
    {
        [JsonProperty("alliances")]
        public Alliances alliances { get; set; }

        [JsonProperty("match_number")]
        public int match_number { get; set; }

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
}
