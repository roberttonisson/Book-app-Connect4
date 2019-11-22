using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;

namespace WebApp.Pages.Comments
{
    public class IndexModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public IndexModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        public IList<Comment> Comment { get;set; } = default!;

        public async Task OnGetAsync(int? bookid)
        {

            if (bookid == null)
            {
                Comment = await _context.Comments
                    .Include(c => c.Book).ToListAsync();
            }
            else
            {
                Comment = await _context.Comments
                    .Include(c => c.Book)
                    .Where(c => c.BookId == bookid)
                    .ToListAsync();
                
            }

        }
    }
}
