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
    }
}