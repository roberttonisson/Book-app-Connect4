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
    public class DeleteModel : PageModel
    {
        private readonly DAL.AppDatabaseContext _context;

        public DeleteModel(DAL.AppDatabaseContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SaveGame = await _context.SaveGames.FindAsync(id);

            if (SaveGame != null)
            {
                _context.SaveGames.Remove(SaveGame);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
