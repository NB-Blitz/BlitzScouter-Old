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
            ViewBag.config = new BSConfig("./config.txt");
            //                  INSECURE \/ \/ \/ \/ \/ \/ \/
            return View(service.getMatch(int.Parse(roundNum)));
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Scout() { return RedirectToAction("Index"); }
    }
}
