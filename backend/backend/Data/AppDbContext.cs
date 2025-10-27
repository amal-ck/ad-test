using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<UserGameLibrary> UserGameLibraries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(e => e.Username).IsUnique();

            modelBuilder.Entity<UserGameLibrary>() //making a composite key- no seperate id is needed beacause no need to purchase a game twice
                .HasKey(ug => new { ug.userId, ug.GameId });

            modelBuilder.Entity<UserGameLibrary>()
                .HasOne(ug => ug.User)
                .WithMany(u => u.UserGameLibraries)
                .HasForeignKey(u => u.userId);

            modelBuilder.Entity<UserGameLibrary>()
                .HasOne(ug => ug.Game)
                .WithMany(g => g.UserGameLibraries)
                .HasForeignKey(ug => ug.GameId);


            base.OnModelCreating(modelBuilder);
        }
    }
}
