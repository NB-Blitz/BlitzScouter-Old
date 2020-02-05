using BlitzScouter.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BlitzScouter.Repository
{
    public class BSRepo
    {
        private readonly BSContext db;

        public BSRepo(BSContext context)
        {
            db = context;
        }

        // The Blue Alliance Integration
        public String getTBA(String query)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.thebluealliance.com/api/v3/" + query);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.Headers["X-TBA-Auth-Key"] = BSConfig.c.tbaApiKey;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        // Get Data
        public BSTeam getTeam(int team)
        {
            return db.BS_Teams.FirstOrDefault(t => t.team == team);
        }
        public bool containsTeam(int team)
        {
            return getTeam(team) != null;
        }
        public bool containsRound(BSRaw m)
        {
            return getById(m.id) != null;
        }
        public List<BSRaw> getRounds(int team)
        {
            var rounds = db.BS_Rounds.Where(t => t.team.Equals(team));
            var arr = rounds.ToList();
            return arr;
        }
        public List<BSRaw> getAll()
        {
            return db.BS_Rounds.ToList();
        }
        public BSRaw getById(int id)
        {
            return db.BS_Rounds.FirstOrDefault(t => t.id == id);
        }

        // Change Data
        public void saveData()
        {
            db.SaveChanges();
        }

        public void addTeam(BSTeam teamData)
        {
            db.BS_Teams.Add(teamData);
            saveData();
        }

        public void addRound(BSRaw roundData)
        {
            db.BS_Rounds.Add(roundData);
            saveData();
        }
        public void updateRound(BSRaw round)
        {
            var dup = db.BS_Rounds.FirstOrDefault(m => m.id == round.id);
            db.Entry(dup).State = EntityState.Detached;
            db.BS_Rounds.Attach(round);
            db.Entry(round).State = EntityState.Modified;
            saveData();
        }
        public void deleteRound(int id)
        {
            var round = db.BS_Rounds.First(r => r.id == id);
            db.BS_Rounds.Remove(round);
            saveData();
        }
    }
}
