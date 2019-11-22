using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAL;
using Domain;

namespace WebApp.Pages.Books
{
    public class CreateModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public CreateModel(DAL.AppDbContext context)
        {
            _context = context;
        }
        
        public SelectList LanguageSelectList { get; set; } = default!;
        public SelectList PublisherSelectList { get; set; } = default!;

        public IActionResult OnGet()
        {
            LanguageSelectList = 
                new SelectList(_context.Languages, nameof(Language.LanguageId), nameof(Language.LanguageName));
            PublisherSelectList = 
                new SelectList(_context.Publishers, nameof(Publisher.PublisherId), nameof(Publisher.PublisherName));
            return Page();
        }

        [BindProperty]
        public Book Book { get; set; }  = default!;

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                LanguageSelectList = 
                    new SelectList(_context.Languages, nameof(Language.LanguageId), nameof(Language.LanguageName));
                PublisherSelectList = 
                    new SelectList(_context.Publishers, nameof(Publisher.PublisherId), nameof(Publisher.PublisherName));
                return Page();
            }

            _context.Books.Add(Book);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
