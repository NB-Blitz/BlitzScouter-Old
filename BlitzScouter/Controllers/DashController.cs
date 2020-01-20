using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzScouter.Models;
using BlitzScouter.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlitzScouter.Controllers
{
    public class DashController : Controller
    {
        private BSService service;

        public DashController(BSContext context)
        {
            service = new BSService(context);
        }

        public IActionResult Index()
        {
            return View();
        }

        // Teams
        public IActionResult Teams(int code)
        {
            BSConfig.initialize();
            ViewBag.code = code;
            return View(service.getAllTeams());
        }
        
        public IActionResult Team(String teamnum, int code)
        {
            int ex;
            bool isNumeric = int.TryParse(teamnum, out ex);
            if (ex < 0)
                ex = -ex;
            if (isNumeric && !service.containsTeam(ex))
                return RedirectToAction("Teams", new { controller = "Dash", action = "Teams", code = 2 });
            BSTeam tm = service.getTeam(ex);
            ViewBag.code = code;
            return View(tm);
        }

        [HttpPost]
        public IActionResult Team(String teamnum, String teamname, String comments)
        {
            int ex;
            bool isNumeric = int.TryParse(teamnum, out ex);
            if (ex < 0)
                ex = -ex;
            BSTeam tm = service.getTeam(ex);
            if (isNumeric && tm != null)
            {
                tm.name = teamname;
                tm.pitComments = comments;
                service.setTeam(tm);
            }
            return RedirectToAction("Team", new { controller = "Dash", action = "Team", teamnum = teamnum, code = 1 });
        }

        // Rounds
        public IActionResult Rounds(int code)
        {
            BSConfig.initialize();
            ViewBag.code = code;
            return View(service.getAllRounds());
        }

        public IActionResult Round(String roundnum)
        {
            int ex;
            bool isNumeric = int.TryParse(roundnum, out ex);
            if (ex < 0)
                ex = -ex;
            BSMatch r = service.getMatch(ex);
            if (isNumeric && r != null)
                return View(r);
            else
                return RedirectToAction("Rounds", new { controller = "Dash", action = "Rounds", code=2 });
        }
    }
}
