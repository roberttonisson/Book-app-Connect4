using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL;
using DOMAIN;
using GameEngine;

namespace WebApp.Pages_GameConfig
{
    public class IndexModel : PageModel
    {
        private readonly DAL.AppDatabaseContext _context;

        public IndexModel(DAL.AppDatabaseContext context)
        {
            _context = context;
        }

        [Display(Name = "Current Settings")] public IList<GameConfig> GameConfig { get; set; } = default!;

        public GameSettings GameSettings { get; set; } = default!;

        public void OnGet()
        {
            //GameConfig = await _context.GameConfig.ToListAsync();
            var dbOptions = new DbContextOptionsBuilder<AppDatabaseContext>()
                .UseSqlite("Data Source=/Users/rober/RiderProjects/icd0008-2019f/Connect4/WebApp/app.db").Options;
            GameSettings = GameConfigHandler.LoadConfig(dbOptions);
        }
    }
}