using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlitzScouter.Models;
using BlitzScouter.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlitzScouter.Controllers
{
    public class MainController : Controller {

        private BSService service;

        public MainController(BSContext context)
        {
            service = new BSService(context);
        }

        public IActionResult Index(int code)
        {
            ViewBag.code = code;
            return View();
        }

        [HttpPost]
        public IActionResult Scout(BSRaw data)
        {
            BSConfig.initialize();
            if (service.containsTeam(data.team))
                return View(data);
            else
                return RedirectToAction("Index", new { controller = "Main", action = "Index", code = 2 });
        }
        
        [HttpPost]
        public IActionResult Data(BSRaw model)
        {
            service.addUserData(model);
            return RedirectToAction("Index", new { controller = "Main", action = "Index", code = 1 });
        }

        // Redirect to Index When Manually Connecting to Scout or Data
        public IActionResult Scout() { return RedirectToAction("Index"); }
        public IActionResult Data() { return RedirectToAction("Index"); }
        
    }
}