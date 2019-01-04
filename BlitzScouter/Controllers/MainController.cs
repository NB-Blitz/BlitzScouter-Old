using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BlitzScouter.Controllers
{
    public class MainController : Controller
    {
        [HttpPost]
        public IActionResult Index(String teamNum, String color)
        {
            return RedirectToAction("Scout");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Scout(String[] teamInfo)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Scout()
        {
            return RedirectToAction("Data");
        }

        public IActionResult Data()
        {
            return View();
        }
    }
}