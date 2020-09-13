﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Poker.Models;

namespace Poker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(new EvaluateHandViewModel()); ;
        }

        [HttpPost]
        [TypeFilter(typeof(CustomExceptionFilter))]
        public IActionResult AutoDeal(EvaluateHandViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("Results", model);
            }  
            
            model.PlayPoker();     

            return PartialView("Results", model);
        }

        public IActionResult About()
        {
            return View();
        }

      
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
