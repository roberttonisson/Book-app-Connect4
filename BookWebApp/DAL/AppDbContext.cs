using System;
using System.Net;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class AppDbContext: DbContext
    {
        public DbSet<Author> Authors { get; set; } = default!;
        public DbSet<Book> Books { get; set; } = default!;
        public DbSet<BookAuthor> BookAuthors { get; set; } = default!;
        public DbSet<Comment> Comments { get; set; } = default!;
        public DbSet<Language> Languages { get; set; } = default!;
        public DbSet<Publisher> Publishers { get; set; } = default!;

        public AppDbContext(DbContextOptions option): base(option)
        {
        }
        
    }
}