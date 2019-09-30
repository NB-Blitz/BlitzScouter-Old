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
        public BSTeam getTeam(string team)
        {
            return db.BS_Teams.FirstOrDefault(t => t.team == team);
        }
        public bool containsTeam(string team)
        {
            return getTeam(team) != null;
        }
        public BSRaw[] getRounds(string team)
        {
            return db.BS_Rounds.Where(t => t.team == team).ToArray<BSRaw>();
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
