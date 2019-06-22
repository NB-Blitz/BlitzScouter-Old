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
        public BSTeam getTeam(string teamNum)
        {
            return db.BlitzScoutingData.FirstOrDefault(t => t.teamNum == teamNum);
            //return db.BlitzScoutingData.FromSql("SELECT * FROM dbo.BlitzScoutingData WHERE teamNum = '@team'", new SqlParameter("team", teamNum)).FirstOrDefault();
        }
        public bool containsTeam(string teamNum)
        {
            return getTeam(teamNum) != null;
        }

        // Change Data
        public void saveData()
        {
            db.SaveChanges();
        }

        public void addData(BSTeam teamData)
        {
            db.BlitzScoutingData.Add(teamData);
            saveData();
        }

        /*public void addUserData(BSRaw model)
        {
            db.BlitzScoutingData.Add(model);
            db.SaveChanges();
        }*/
    }
}
