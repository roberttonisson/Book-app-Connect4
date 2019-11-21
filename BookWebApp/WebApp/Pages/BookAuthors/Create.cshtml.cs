using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAL;
using Domain;

namespace WebApp.Pages.BookAuthors
{
    public class CreateModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public CreateModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        public SelectList AuthorSelectList { get; set; } = default!;
        public SelectList BookSelectList { get; set; } = default!;

        public IActionResult OnGet()
        {
            AuthorSelectList = 
                new SelectList(_context.Authors, nameof(Author.AuthorId), nameof(Author.FirstName));
            BookSelectList = 
                new SelectList(_context.Books, nameof(Book.BookId), nameof(Book.Title));
            return Page();
        }

        [BindProperty] public BookAuthor BookAuthor { get; set; }  = default!;

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

            _context.BookAuthors.Add(BookAuthor);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}