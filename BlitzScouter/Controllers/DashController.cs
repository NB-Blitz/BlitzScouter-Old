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
        public IActionResult Teams()
        {
            BSConfig.initialize();
            return View(service.getAllTeams());
        }
        
        public IActionResult Team(String teamnum)
        {
            int ex;
            bool isNumeric = int.TryParse(teamnum, out ex);
            if (ex < 0)
                ex = -ex;
            BSTeam tm = service.getTeam(ex);
            if (isNumeric && tm != null)
                return View(tm);
            else
                return View(null);
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
            return RedirectToAction("Index", new { controller = "Dash", action = "Team", teamnum = teamnum });
        }

        // Rounds
        public IActionResult Rounds(String msg)
        {
            BSConfig.initialize();
            ViewBag.msg = msg;
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
                return RedirectToAction("Index", new { controller = "Dash", action = "Rounds", msg = "Invalid Match" });
        }
    }
}
