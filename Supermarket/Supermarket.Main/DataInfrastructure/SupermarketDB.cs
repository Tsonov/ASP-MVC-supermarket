using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<CashDesk> CashDesk { get; set; }
        public DbSet<Replenishment> Replenishments { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleDetail> SaleDetails { get; set; }
        public DbSet<ReplenishmentDetail> ReplenishmentDetails { get; set; }
        public DbSet<ProductAvailability> ProductAvailabilities { get; set; }
        public DbSet<ProductAvailabilityDetail> ProductAvailabilitiesDetails { get; set; }
    }
}