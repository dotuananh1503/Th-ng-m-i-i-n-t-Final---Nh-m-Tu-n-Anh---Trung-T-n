using Huflix.Areas.Admin.Models;
using Huflix.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huflix.Areas.Admin.Data
{
    public class DataContext:IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ContactModel> Contacts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<DetailReceipt> DetailReceipts { get; set; }
        public DbSet<Product> Products { get; set; }
      
        public DbSet<Receipt> Receipts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
           

            builder.Entity<IdentityRole>().HasData(
               new IdentityRole
               {
                   Name = "Visitor",
                   NormalizedName = "VISITOR"
               },
               new IdentityRole
               {
                   Name = "Administrator",
                   NormalizedName = "ADMINISTRATOR"
               });


        }
    }
}
