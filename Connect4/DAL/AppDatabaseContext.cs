using System;
using DOMAIN;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace DAL
{
    public class AppDatabaseContext : DbContext
    {
        public DbSet<SaveGame> SaveGames { get; set; } = default!;
        public DbSet<GameConfig> GameConfig { get; set; } = default!;
        

        private static readonly ILoggerFactory MyLoggerFactory
            = LoggerFactory.Create(builder => { builder.AddConsole(); });

        /* protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
         {
             optionsBuilder
                 .UseLoggerFactory(MyLoggerFactory)
                 .UseSqlite("Data Source=/Users/rober/RiderProjects/app.db");
         }*/
        public AppDatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}