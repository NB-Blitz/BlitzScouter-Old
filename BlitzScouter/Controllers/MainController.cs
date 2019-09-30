using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    
        [HttpPost]
        public IActionResult Team(BSTeamMod mod)
        {
            if (mod.team.team != mod.prevTeam)
            {
                return View(service.getMod(mod.team.team));
            }
            else
            {
                
                service.setTeam(mod.team);
                return View(service.getMod(mod.team.team));
            }
        }

        public IActionResult Team()
        {
            return View(service.getMod("5148"));
        }

        // Redirect to Index When Manually Connecting to Scout or Data
        public IActionResult Scout() { return RedirectToAction("Index"); }
        public IActionResult Data() { return RedirectToAction("Index"); }
        
    }
}