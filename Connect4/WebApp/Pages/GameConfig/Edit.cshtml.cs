using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using DOMAIN;
using GameEngine;

namespace WebApp.Pages_GameConfig
{
    public class EditModel : PageModel
    {
        private readonly DAL.AppDatabaseContext _context;

        public EditModel(DAL.AppDatabaseContext context)
        {
            _context = context;
            _GameSettings = new GameSettings();
        }

        [BindProperty] public GameConfig GameConfig { get; set; } = default!;

        [BindProperty]
        [Display(Name = "Board Height")]
        [Range(4, 100)]
        public int BoardHeight { get; set; }

        [BindProperty]
        [Display(Name = "Board Width")]
        [Range(4, 100)]
        public int BoardWidth { get; set; }

        public GameSettings _GameSettings { get; set; }
        

        public IActionResult  OnPost()
        {
            var dbOptions = new DbContextOptionsBuilder<AppDatabaseContext>()
                .UseSqlite("Data Source=/Users/rober/RiderProjects/icd0008-2019f/Connect4/WebApp/app.db").Options;
            _GameSettings.BoardHeight = BoardHeight;
            _GameSettings.BoardWidth = BoardWidth;
            GameConfigHandler.SaveConfig(_GameSettings, dbOptions);
            return new RedirectToPageResult("Index");
        }
    }
}