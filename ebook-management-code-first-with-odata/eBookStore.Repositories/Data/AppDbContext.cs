using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using eBookStore.Domains.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace eBookStore.Repositories.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }
        public DbSet<Role> Role { get; set; } = default!;
        public DbSet<User> User { get; set; } = default!;
        public DbSet<Author> Author { get; set; } = default!;
        public DbSet<Book> Book { get; set; } = default!;
        public DbSet<BookAuthor> BookAuthor { get; set; } = default!;
        public DbSet<Publisher> Publisher { get; set; } = default!;

        /*private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            return configuration.GetConnectionString("MyDB");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(GetConnectionString());
            }
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasMany(r => r.Users)
            .WithOne(u => u.Role)
            .HasForeignKey(u => u.RoleId);

            modelBuilder.Entity<User>().HasOne(u => u.Role)
            .WithMany(x => x.Users)
            .HasForeignKey(u => u.RoleId);

            modelBuilder.Entity<Author>().HasMany(a => a.BookAuthors)
            .WithOne(ba => ba.Author)
            .HasForeignKey(ba => ba.AuthorId);

            modelBuilder.Entity<Book>().HasMany(b => b.BookAuthors)
            .WithOne(ba => ba.Book)
            .HasForeignKey(ba => ba.BookId);
            modelBuilder.Entity<Book>().HasOne(x => x.Publisher)
            .WithMany(x => x.Books)
            .HasForeignKey(x => x.PublisherId);
            modelBuilder.Entity<User>().HasOne(x => x.Publisher)
            .WithMany(p => p.Users)
            .HasForeignKey(x => x.PublisherId);
            base.OnModelCreating(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(new Role
            {
                RoleName = "Admin",
                Id = Guid.NewGuid(),

            }, new Role
            {
                RoleName = "Customer",
                Id = Guid.NewGuid()
            });
        }


    }
}
