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
        public IActionResult Scout(BSTeam mod)
        {
            BSConfig.initialize();
            return View(service.getTeam(mod.team));
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Scout() { return RedirectToAction("Index"); }
    }
}