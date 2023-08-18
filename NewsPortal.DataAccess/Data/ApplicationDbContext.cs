using Microsoft.EntityFrameworkCore;
using NewsPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace NewsPortal.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Politics", DisplayOrder = 1 },
                new Category { Id = 2, Name = "World", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Opinion", DisplayOrder = 3 }
                );
        }
    }
}
