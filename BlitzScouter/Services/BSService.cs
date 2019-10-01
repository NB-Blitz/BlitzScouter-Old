﻿using BlitzScouter.Models;
using BlitzScouter.Repository;
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

        public BSTeam getTeam(String team)
        {
            return repo.getTeam(team);
        }

        public BSRaw[] getRounds(String team)
        {
            return repo.getRounds(team);
        }
    }
}
