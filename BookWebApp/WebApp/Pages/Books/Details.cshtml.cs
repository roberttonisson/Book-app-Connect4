using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;

namespace WebApp.Pages.Books
{
    public class DetailsModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public DetailsModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        public Book Book { get; set; }  = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Book = await _context.Books
                .Include(b => b.Language)
                .Include(b => b.Publisher)
                .Include(b => b.BookAuthors).ThenInclude(a => a.Author)
                .FirstOrDefaultAsync(m => m.BookId == id);
                

            if (Book == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
