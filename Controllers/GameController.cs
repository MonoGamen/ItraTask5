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
    public class GameController : Controller
    {
        GameCollection _gameCollection;
        public GameController(GameCollection gameCollection)
        {
            _gameCollection = gameCollection;
        }

        public IActionResult Index(int? id)
        {
            if (!id.HasValue)
                return NotFound();
            Game game = _gameCollection.Games.FirstOrDefault(g => g.GameId == id.Value);
            if (game == null || game.IsFull || game.IsEnded)
                return NotFound();

            if (!game.IsStarted)
            { 
                HttpContext.Response.Cookies.Append("player", "First");
                game.IsStarted = true;
                return View();
            }
            else
            { 
                HttpContext.Response.Cookies.Append("player", "Second");
                game.IsFull = true;
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
