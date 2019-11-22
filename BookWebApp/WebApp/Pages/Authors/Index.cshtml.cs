using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;

namespace WebApp.Pages.Authors
{
    public class IndexModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public IndexModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        public IList<Author> Author { get; set; } = default!;


        public async Task OnGetAsync(string? orderBy, string? descending)
        {
            var temp = _context.Authors;
            var desc = descending == "true";
            switch (orderBy)
            {
                case "yearofbirth":
                    if (desc)
                    {
                        Author = await temp.OrderByDescending(a => a.YearOfBirth).ToListAsync();
                    }
                    else
                    {
                        Author = await temp.OrderBy(a => a.YearOfBirth).ToListAsync();
                    }

                    break;
                case "firstname":
                    if (desc)
                    {
                        Author = await temp.OrderByDescending(a => a.FirstName).ToListAsync();
                    }
                    else
                    {
                        Author = await temp.OrderBy(a => a.FirstName).ToListAsync();
                    }

                    break;
                default:
                    if (desc)
                    {
                        Author = await temp.OrderByDescending(a => a.LastName).ToListAsync();
                    }
                    else
                    {
                        Author = await temp.OrderBy(a => a.LastName).ToListAsync();
                    }

                    break;
            }
        }
    }
}