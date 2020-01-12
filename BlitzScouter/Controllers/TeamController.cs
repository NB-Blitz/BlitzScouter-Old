using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzScouter.Models;
using BlitzScouter.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlitzScouter.Controllers
{
    public class TeamController : Controller
    {
        private BSService service;

        public TeamController(BSContext context)
        {
            service = new BSService(context);
        }

        [HttpPost]
        public IActionResult Data(BSTeam data)
        {
            service.setTeam(data);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Scout(String teamNum)
        {
            BSConfig.initialize();

            int ex;
            bool isNumeric = int.TryParse(teamNum, out ex);
            if (ex < 0)
                ex = -ex;
            BSTeam tm = service.getTeam(ex);
            if (isNumeric && tm != null)
                return View(tm);
            else
                return RedirectToAction("Index", new { controller = "Team", action = "Index", msg = "Invalid Team" });
        }

        public IActionResult Index(String msg)
        {
            if (msg != null)
                ViewBag.msg = msg;
            else
                ViewBag.msg = "Team Number";
            return View();
        }

        public IActionResult Scout() { return RedirectToAction("Index"); }
    }
}