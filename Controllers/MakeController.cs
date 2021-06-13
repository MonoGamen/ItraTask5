using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Classes;
using TicTacToe.Models;

namespace TicTacToe.Controllers
{
    public class MakeController : Controller
    {
        GameCollection _gameCollection;
        TagCollection _tagCollection;
        public MakeController(GameCollection gameCollection, TagCollection tagCollection)
        {
            _gameCollection = gameCollection;
            _tagCollection = tagCollection;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_tagCollection.Tags);
        }

        [HttpPost]
        public IActionResult Index(string name, string[] tags)
        {
            if (name == null || name == "")
                return RedirectToAction("Index");

            Game game = new Game(_gameCollection.GenerateGameId(), name);
            foreach (string tag in tags)
            {
                game.Tags.Add(tag);
                _tagCollection.Tags.Add(tag);
            }
            _gameCollection.Games.Add(game);
            return RedirectPermanent($"/Game/Index/{game.GameId}");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
