using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL;
using DOMAIN;

namespace WebApp.Pages_SaveGames
{
    public class IndexModel : PageModel
    {
        private readonly DAL.AppDatabaseContext _context;

        public IndexModel(DAL.AppDatabaseContext context)
        {
            _context = context;
        }

        public IList<SaveGame> SaveGame { get;set; } = default!;

        public async Task OnGetAsync()
        {
            SaveGame = await _context.SaveGames.ToListAsync();
        }
    }
}
