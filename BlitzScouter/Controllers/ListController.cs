using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzScouter.Models;
using BlitzScouter.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlitzScouter.Controllers
{
    public class ListController : Controller
    {
        private BSService service;

        public ListController(BSContext context)
        {
            service = new BSService(context);
        }

        public IActionResult Index()
        {
            BSConfig.initialize();
            return View(service.getAllTeams());
        }
    }
}
