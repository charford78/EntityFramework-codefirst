using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDbLib.Models
{
    public class SalesDbContext : DbContext
    {
        // DbSets go here
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Customer>(e =>
            {
                e.HasIndex(p => p.Code).IsUnique(true);//passing in 'true' is not
                                                       //necessary. it will default
                                                       //to true.  Just showing for
                                                       //illustration purposes.
                e.Property(p => p.Name).HasMaxLength(30).
                IsRequired(true);//this does the same thing we did with attributes in
                                 //Customer class.  This fluent api will override
                                 //the attributes though.
            });
        }
        
        public SalesDbContext() {}

        public SalesDbContext(DbContextOptions<SalesDbContext> options)
            : base(options)
        {}

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                var connStr = "server=localhost\\sqlexpress;database=EfSalesDb;" +
                    " trusted_connection=true;";
                builder.UseSqlServer(connStr);
            }
        }
    }
}
