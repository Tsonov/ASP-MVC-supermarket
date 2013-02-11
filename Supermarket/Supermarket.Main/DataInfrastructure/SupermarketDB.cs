using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Supermarket.Core.Models;

namespace Supermarket.Main.DataInfrastructure
{
    public class SupermarketDB : DbContext
    {
        public SupermarketDB() : base("DefaultConnection")
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<CashDesk> CashDesk { get; set; }
        public DbSet<ProductReplenishment> ProductReplenishmets { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleDetail> SaleDetails { get; set; }
        public DbSet<ReplenishmentDetail> ReplenishmentDetails { get; set; }
        public DbSet<ProductInStock> ProductsInStock { get; set; }
        public DbSet<ProductHistory> ProductHistories { get; set; }
        public DbSet<HistoryProductInfo> HistoryProductInfos { get; set; }
    }
}