using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzScouter.Models;
using BlitzScouter.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlitzScouter.Controllers
{
    public class HubController : Controller
    {
        private BSService service;

        public HubController(BSContext context)
        {
            service = new BSService(context);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
