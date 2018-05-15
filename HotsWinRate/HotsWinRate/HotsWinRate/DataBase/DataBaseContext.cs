using HotsWinRate.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotsWinRate.DataBase
{
    public class DataBaseContext : DbContext
    {
        public DbSet<Hero> Heroes { get; set; }

        private readonly string _databasePath;

        public DataBaseContext(string databasePath)
        {
            _databasePath = databasePath;
            Database.EnsureCreated();
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_databasePath}");
        }
    }
}
