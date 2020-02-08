using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzScouter.Models;
using BlitzScouter.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

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
            BSConfig.initialize();
            ViewBag.upcomingRounds = service.getUpcomingRounds();
            List<BSTeam> tms = service.getAllTeams();
            List<PlotData> list = new List<PlotData>();
            foreach (BSTeam tm in tms)
            {
                if (tm.counterAverages == null)
                    continue;
                if (tm.counterAverages.Count < 3)
                    continue;
                list.Add(new PlotData
                {
                    Name = tm.team.ToString(),
                    X = tm.counterAverages[2],
                    Y = tm.counterAverages[1]
                });
            }
            ViewBag.graphData = list;
            return View();
        }

        // Teams
        public IActionResult Teams(int code)
        {
            BSConfig.initialize();
            ViewBag.upcomingRounds = service.getUpcomingRounds();
            ViewBag.code = code;
            return View(service.getAllTeams());
        }
        
        public IActionResult Team(String teamnum, int code)
        {
            BSConfig.initialize();
            ViewBag.upcomingRounds = service.getUpcomingRounds();
            int ex;
            bool isNumeric = int.TryParse(teamnum, out ex);
            if (ex < 0)
                ex = -ex;
            if (isNumeric && !service.containsTeam(ex))
                return RedirectToAction("Teams", new { controller = "Dash", action = "Teams", code = 2 });
            BSTeam tm = service.getTeam(ex);
            ViewBag.code = code;

            ViewBag.graphDataA = tm.rounds;

            return View(tm);
        }

        [HttpPost]
        public IActionResult Team(String teamnum, String teamname, String comments)
        {
            BSConfig.initialize();
            ViewBag.upcomingRounds = service.getUpcomingRounds();
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
            ViewBag.upcomingRounds = service.getUpcomingRounds();
            ViewBag.code = code;
            return View(service.getAllRounds());
        }

        public IActionResult Round(String roundnum, String raw)
        {
            BSConfig.initialize();
            ViewBag.upcomingRounds = service.getUpcomingRounds();
            BSMatch r;
            if (raw == null)
            {
                r = service.getMatch("qm" + roundnum);
            }
            else
            {
                r = service.getMatch(raw);
            }
            if (r != null)
                return View(r);
            else
                return RedirectToAction("Rounds", new { controller = "Dash", action = "Rounds", code=2 });
        }

        [HttpPost]
        public IActionResult Edit(BSRaw raw)
        {
            BSConfig.initialize();
            ViewBag.upcomingRounds = service.getUpcomingRounds();
            service.setRound(raw);
            ViewBag.code = 1;
            BSRaw r = service.getById(raw.id);
            if (r == null)
            {
                return RedirectToAction("Rounds", new { controller = "Dash", code = 3 });
            }
            else
            {
                return View(r);
            }
        }

        public IActionResult Edit(int id)
        {
            BSConfig.initialize();
            ViewBag.upcomingRounds = service.getUpcomingRounds();
            BSRaw r = service.getById(id);
            if (r == null)
            {
                return RedirectToAction("Rounds", new { controller = "Dash", code = 3 });
            }
            else
            {
                return View(r);
            }
        }

        public IActionResult Delete(int id)
        {
            BSConfig.initialize();
            ViewBag.upcomingRounds = service.getUpcomingRounds();
            service.deleteRound(id);
            return RedirectToAction("Rounds", new { controller = "Dash", code = 4 });
        }
    }
    public class PlotData
    {
        public double X { get; set; }
        public double Y { get; set; }
        public String Name { get; set; }
    }

}
