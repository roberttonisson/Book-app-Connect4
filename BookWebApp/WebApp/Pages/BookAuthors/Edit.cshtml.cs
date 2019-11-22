using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;

namespace WebApp.Pages.BookAuthors
{
    public class EditModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public EditModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BookAuthor BookAuthor { get; set; } = default!;
        
        public SelectList AuthorSelectList { get; set; } = default!;
        public SelectList BookSelectList { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BookAuthor = await _context.BookAuthors
                .Include(b => b.Author)
                .Include(b => b.Book).FirstOrDefaultAsync(m => m.BookAuthorId == id);

            if (BookAuthor == null)
            {
                return NotFound();
            }
            AuthorSelectList = 
                new SelectList(_context.Authors, nameof(Author.AuthorId), nameof(Author.FirstName));
            BookSelectList = 
                new SelectList(_context.Books, nameof(Book.BookId), nameof(Book.Title));
            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                AuthorSelectList = 
                    new SelectList(_context.Authors, nameof(Author.AuthorId), nameof(Author.FirstName));
                BookSelectList = 
                    new SelectList(_context.Books, nameof(Book.BookId), nameof(Book.Title));
                return Page();
            }

            _context.Attach(BookAuthor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookAuthorExists(BookAuthor.BookAuthorId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BookAuthorExists(int id)
        {
            return _context.BookAuthors.Any(e => e.BookAuthorId == id);
        }
    }
}
