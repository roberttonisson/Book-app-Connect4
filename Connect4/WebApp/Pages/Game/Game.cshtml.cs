using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using GameEngine;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.Game
{
    public class GameModel : PageModel
    {
        public GameEngine.Game Game { get; set; } = default!;
        public int GameId { get; set; } = default!;
        public string Winner { get; set; } = default!;
        public bool GameIsOver = false; 

        public readonly DbContextOptions DbOptions = new DbContextOptionsBuilder<AppDatabaseContext>()
            .UseSqlite("Data Source=/Users/rober/RiderProjects/icd0008-2019f/Connect4/WebApp/app.db").Options;

        [BindProperty] public int SelectedRow { get; set; }


        public IActionResult OnGet(int? SaveGameId, string gameOver)
        {
            if (SaveGameId == null)
            {
                return NotFound();
            }

            if (gameOver != null)
            {
                Winner = gameOver;
                GameIsOver = true;
            }

            GameId = SaveGameId.Value;

            Game = GameConfigHandler.LoadGameInWeb(DbOptions, GameId);
            
            if (Game._playerZeroMove && Game._computerPlays && gameOver == null)
            {
                if (Game.ComputerMove())
                {
                    GameIsOver = true;
                    Winner = "red";
                }

                if (Game.ColumnStatus.Values.ToList().Aggregate(0, (current, value) => current + value) >=
                    Game.BoardHeight * Game.BoardWidth && !GameIsOver)
                {
                    GameIsOver = true;
                    Winner = "nobody";
                    return RedirectToPage("Index");
                }

                GameConfigHandler.SaveInWeb(Game, DbOptions, GameId);

                if (GameIsOver)
                {
                    return new RedirectResult("./Game?SaveGameId=" + GameId + "&gameOver=" + Winner);
                }
            }

            return Page();
        }

        public IActionResult OnPost(int selectedRow, int id)
        {
            GameId = id;
            Game = GameConfigHandler.LoadGameInWeb(DbOptions, GameId);
            if (Game.ColumnStatus[selectedRow] >= Game.BoardHeight)
            {
                return new RedirectResult("./Game?SaveGameId=" + GameId);
            }

            if (Game.Move(selectedRow))
            {
                GameIsOver = true;
                Winner = Game._playerZeroMove ? "Blue" : "Red";
            }

            if (Game.ColumnStatus.Values.ToList().Aggregate(0, (current, value) => current + value) >=
                Game.BoardHeight * Game.BoardWidth && !GameIsOver)
            {
                GameIsOver = true;
                Winner = "nobody";
            }

            GameConfigHandler.SaveInWeb(Game, DbOptions, GameId);
            
            if (GameIsOver)
            {
                return new RedirectResult("./Game?SaveGameId=" + GameId + "&gameOver=" + Winner);
            }
            return new RedirectResult("./Game?SaveGameId=" + GameId);
        }
    }
}