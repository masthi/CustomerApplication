using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CustomerApplication.Models;

namespace CustomerApplication.DAL
{
    public class DataAccessLayer:DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<User>().ToTable("Users");
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<User> Users { get; set; }
    }
}