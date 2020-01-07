using BlitzScouter.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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

        // Get Data
        public BSTeam getTeam(int team)
        {
            return db.BS_Teams.FirstOrDefault(t => t.team == team);
        }
        public bool containsTeam(int team)
        {
            return getTeam(team) != null;
        }
        public BSRaw[] getRounds(int team)
        {
            var rounds = db.BS_Rounds.Where(t => t.team.Equals(team));
            var arr = rounds.ToArray();
            return arr;
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
    }
}
