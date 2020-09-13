using System;
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
            return View(new EvaluateHandViewModel());            
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

#if DEBUG
        public IActionResult TestHand()
        {
            return View(new EvaluateHandViewModel());
        }

        [HttpPost]
        [TypeFilter(typeof(CustomExceptionFilter))]
        public IActionResult TestHand(EvaluateHandViewModel model)
        {
            
            var hands = new Card[][]
            {
                new Card[] {
                    new Card(Suit.Clubs, Rank.Eight),
                    new Card(Suit.Clubs, Rank.Ten),
                    new Card(Suit.Clubs, Rank.Three),
                    new Card(Suit.Clubs, Rank.Nine),
                    new Card(Suit.Clubs, Rank.Ace),
                },
                new Card[] {
                    new Card(Suit.Clubs, Rank.Three),
                    new Card(Suit.Diamonds, Rank.Three),
                    new Card(Suit.Hearts, Rank.Two),
                    new Card(Suit.Spades, Rank.Three),
                    new Card(Suit.Spades, Rank.Two),
                },
                new Card[] {
                    new Card(Suit.Clubs, Rank.Eight),
                    new Card(Suit.Diamonds, Rank.Eight),
                    new Card(Suit.Spades, Rank.Eight),
                    new Card(Suit.Hearts, Rank.Eight),
                    new Card(Suit.Clubs, Rank.Ace),
                }
            };

            model.PlayCustomHands(hands);            

            return PartialView("Results", model);

        }

#endif

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
