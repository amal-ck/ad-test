﻿using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(e => e.Username).IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
