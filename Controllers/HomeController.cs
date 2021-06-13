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
    public class HomeController : Controller
    {
        GameCollection _gameCollection;
        TagCollection _tagCollection;
        public HomeController(GameCollection gameCollection, TagCollection tagCollection)
        {
            _gameCollection = gameCollection;
            _tagCollection = tagCollection;
        }

        private List<GameModel> GetGames()
        {
            var games = from g in _gameCollection.Games
            where g.IsStarted && !g.IsFull && !g.IsEnded
            select new GameModel() { GameId = g.GameId, Name = g.Name, Tags = g.Tags };
            return games.ToList();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index()
        {
            return View(new HomeModel() { Games = GetGames(), Tags = _tagCollection.Tags });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
