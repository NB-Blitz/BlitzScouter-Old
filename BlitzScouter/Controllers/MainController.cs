using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlitzScouter.Models;
using BlitzScouter.Services;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace BlitzScouter.Controllers
{
    public class MainController : Controller {

        private BSService service;

        public MainController(BSContext context)
        {
            service = new BSService(context);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Scout(BSRaw data)
        {
            return View(data);
        }
        
        [HttpPost]
        public IActionResult Data(BSRaw model)
        {
            service.addUserData(model);
            return View();
        }

        public IActionResult Admin()
        {
            return View();
        }

        // Redirect to Index When Manually Connecting to Scout or Data
        public IActionResult Scout() { return RedirectToAction("Index"); }
        public IActionResult Data() { return RedirectToAction("Index"); }
    }
}