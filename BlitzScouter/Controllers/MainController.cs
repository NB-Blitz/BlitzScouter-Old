using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlitzScouter.Models;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace BlitzScouter.Controllers
{
    public class MainController : Controller {

        private readonly BSContext db;

        public MainController(BSContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Scout(DataModel data)
        {
            return View(data);
        }
        
        [HttpPost]
        public IActionResult Data(DataModel model)
        {
            db.BlitzScoutingData.Add(model);
            db.SaveChanges();
            return View();
        }

        // Redirect to Index When Manually Connecting to Scout or Data
        public IActionResult Scout() { return RedirectToAction("Index"); }
        public IActionResult Data() { return RedirectToAction("Index"); }
    }
}