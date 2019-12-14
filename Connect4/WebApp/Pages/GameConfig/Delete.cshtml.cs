using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL;
using DOMAIN;

namespace WebApp.Pages_GameConfig
{
    public class DeleteModel : PageModel
    {
        private readonly DAL.AppDatabaseContext _context;

        public DeleteModel(DAL.AppDatabaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        public GameConfig GameConfig { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            GameConfig = await _context.GameConfig.FirstOrDefaultAsync(m => m.GameConfigId == id);

            if (GameConfig == null)
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

            GameConfig = await _context.GameConfig.FindAsync(id);

            if (GameConfig != null)
            {
                _context.GameConfig.Remove(GameConfig);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
