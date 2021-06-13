using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.Classes
{
    public class GameHub : Hub
    {
        GameCollection _gameCollection;
        public GameHub(GameCollection gameCollection)
        {
            _gameCollection = gameCollection;
        }

        private Game GetGame(HubCallerContext context)
        { 
            string gameid = context.GetHttpContext().Request.Query["userurl"].ToString().Split("/").Last();
            return _gameCollection.Games.First(g => g.GameId == int.Parse(gameid));
        }

        private async Task CheckEndGame(int end, Game game)
        {
            if (end != 0)
            {
                if (end == 1)
                    await Clients.Group(game.GameId.ToString()).SendAsync("gameEnd", "Cross wins!");
                else if (end == 2)
                    await Clients.Group(game.GameId.ToString()).SendAsync("gameEnd", "Zeros wins!");
                else 
                    await Clients.Group(game.GameId.ToString()).SendAsync("gameEnd", "Dead Heat!");
                game.IsEnded = true;
            }
        }

        public async Task Move(string x, string y, string id)
        {
            Game game = GetGame(Context);
            int move = game.MakeMove(int.Parse(x), int.Parse(y), id); 
            if (move != 0)
            {
                await Clients.Group(game.GameId.ToString()).SendAsync("draw", x, y, move == 1 ? "Cross" : "Zero");
                await Task.Delay(200);
                await CheckEndGame(game.CheckGameEnd(), game);
            }
        }

        public override async Task OnConnectedAsync()
        {
            Game game = GetGame(Context);
            await Groups.AddToGroupAsync(Context.ConnectionId, game.GameId.ToString());
            
            if (game.IsFull)
            {
                var firstMove = game.GetFirstMove();
                if (firstMove.HasValue)
                    await Clients.Caller.SendAsync("draw", firstMove.Value.Item2, firstMove.Value.Item2, "Cross");
            }
            await base.OnConnectedAsync();
        }


        public override async Task OnDisconnectedAsync(Exception e)
        {
            Game game = GetGame(Context);
            if (!game.IsEnded)
            {
                game.IsEnded = true;
                await Clients.Group(game.GameId.ToString()).SendAsync("gameEnd", "Error, the enemy came out");
            }
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, game.GameId.ToString());
            await base.OnDisconnectedAsync(e);
        }
    }
}
