﻿using Microsoft.EntityFrameworkCore;

using CarShop.Data.Models;

namespace CarShop.Data
{
    public class CarShopDbContex : DbContext
    {
        public DbSet<User> Users { get; init; }

        public DbSet<Car> Cars { get; init; }
        
        public DbSet<Issue> Issues { get; init; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=CarShop;Integrated Security=True;");
            }
        }
    }
}
