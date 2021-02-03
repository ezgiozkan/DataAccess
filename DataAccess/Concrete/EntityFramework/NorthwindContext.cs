using Entitites.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    // Context : DB tabloları ile proje classlarını bağlamak 
    class NorthwindContext : DbContext
    {
        //
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Normal projelerde Ip verilir
            //optionsBuilder.UseSqlServer(@"Server = 175.45.2.12");

            optionsBuilder.UseSqlServer(@"Server = (localdb)\mssqllocaldb;Database=Northwind;Trusted_Connection = true");

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }



    }
}
