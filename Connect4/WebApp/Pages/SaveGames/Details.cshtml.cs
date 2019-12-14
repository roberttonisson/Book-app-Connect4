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
    public class DetailsModel : PageModel
    {
        private readonly DAL.AppDatabaseContext _context;

        public DetailsModel(DAL.AppDatabaseContext context)
        {
            _context = context;
        }

        public SaveGame SaveGame { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SaveGame = await _context.SaveGames.FirstOrDefaultAsync(m => m.SaveGameId == id);

            if (SaveGame == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
