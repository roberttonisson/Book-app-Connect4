using System.ComponentModel.DataAnnotations;
using System.Linq;
using DAL;
using GameEngine;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.GameStart
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        [Display(Name = "Choose a name for your game save file:")]
        [MinLength(4, ErrorMessage = "Name should be at least 4-100 character long.")]
        [MaxLength(100, ErrorMessage = "Name should be at least 4-100 character long.")]
        public string SaveName { get; set; } = default!;

        public bool ComputerPlays { get; set; } = default!;
        public bool ComputerStarts { get; set; } = default!;

        [BindProperty] public string GameMode { get; set; } = default!;
        public string[] GameModes = {"Human Starts", "Computer Starts", "Human Against Human"};

        public void OnGet(int idToRemove, string finished)
        {
            var dbOptions = new DbContextOptionsBuilder<AppDatabaseContext>()
                .UseSqlite("Data Source=/Users/rober/RiderProjects/icd0008-2019f/Connect4/WebApp/app.db").Options;
            if (finished != null)
            {
                var ctx = new AppDatabaseContext(dbOptions);
                using (ctx)
                {
                    var saveGame = ctx.SaveGames.FirstOrDefault(n => n.SaveGameId == idToRemove);
                    if (saveGame != null)
                    {
                        ctx.SaveGames.Remove(saveGame);
                    }

                    ctx.SaveChanges();
                }
            }
        }

        public IActionResult OnPost()
        {
            var dbOptions = new DbContextOptionsBuilder<AppDatabaseContext>()
                .UseSqlite("Data Source=/Users/rober/RiderProjects/icd0008-2019f/Connect4/WebApp/app.db").Options;
            var gameSettings = GameConfigHandler.LoadConfig(dbOptions);
            if (GameMode == GameModes[0])
            {
                ComputerPlays = true;
                ComputerStarts = false;
            }
            else if (GameMode == GameModes[1])
            {
                ComputerPlays = true;
                ComputerStarts = true;
            }
            else if (GameMode == GameModes[2])
            {
                ComputerPlays = false;
                ComputerStarts = false;
            }

            var game = new GameEngine.Game(gameSettings, ComputerPlays, ComputerStarts);
            var id = GameConfigHandler.SaveInWeb(game, dbOptions, -1, SaveName);
            return new RedirectResult("./Game/Game?SaveGameId=" + id);
        }
    }
}