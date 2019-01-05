using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BlitzScouter.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Scout(Models.DataModel data)
        {
            return View(data);
        }
        
        [HttpPost]
        public IActionResult Data(Models.DataModel model)
        {
            return View();
        }

        // Redirect to Index When Manually Connecting to Scout or Data
        public IActionResult Scout() { return RedirectToAction("Index"); }
        public IActionResult Data() { return RedirectToAction("Index"); }
    }
}