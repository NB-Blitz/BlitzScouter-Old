using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzScouter.Models;
using BlitzScouter.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlitzScouter.Controllers
{
    public class RoundController : Controller
    {
        private BSService service;

        public RoundController(BSContext context)
        {
            service = new BSService(context);
        }

        [HttpPost]
        public IActionResult Scout(String roundNum)
        {
            BSConfig.initialize();

            int ex;
            bool isNumeric = int.TryParse(roundNum, out ex);
            BSMatch match = service.getMatch(ex);
            if (isNumeric && match != null)
                return View(match);
            else
                return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Scout() { return RedirectToAction("Index"); }
    }
}
