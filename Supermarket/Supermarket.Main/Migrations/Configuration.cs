namespace Supermarket.Main.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Supermarket.Core.Models;
    using System.Web.Security;
    using WebMatrix.WebData;
    using System.Collections;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<Supermarket.Main.DataInfrastructure.SupermarketDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Supermarket.Main.DataInfrastructure.SupermarketDB context)
        {
            if (context.CashDesk.Count() == 0)
            {
                context.CashDesk.Add(new CashDesk() { AvailableAmount = 1000 });
            }

            context.Categories.AddOrUpdate(d => d.Name,
                new Category { Name = "Beverages", IsActive = true },
                new Category { Name = "Food", IsActive = true });

            context.SaveChanges();
            Product borovec = new Product
                {
                    Name = "Borovec",
                    CategoryId = context.Categories.Single(cat => cat.Name == "Food").Id,
                    Manufacturer = "Pobeda",
                    Price = 1.5m,
                    UnitMeasure = "broi",
                    IsActive = true,
                };
            Product cocaCola = new Product
                {
                    Name = "Coca-cola",
                    CategoryId = context.Categories.Single(cat => cat.Name == "Beverages").Id,
                    Manufacturer = "Coca-Cola",
                    Price = 2.5m,
                    UnitMeasure = "l",
                    IsActive = true,
                };
            context.Products.AddOrUpdate(d => d.Name,
                borovec, cocaCola);

            if (context.ProductAvailabilities.Count() == 0)
            {
                ProductAvailability availability = new ProductAvailability()
                {
                    Date = DateTime.Now.Date,
                    ProductInfos = new HashSet<ProductAvailabilityDetail>()
                };
                availability.ProductInfos.Add(new ProductAvailabilityDetail()
                {
                    Amount = 5,
                    Product = borovec,
                });
                availability.ProductInfos.Add(new ProductAvailabilityDetail()
                {
                    Amount = 5,
                    Product = cocaCola,
                });
                context.ProductAvailabilities.Add(availability);
            }


            WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);

            var roleProvider = (SimpleRoleProvider)Roles.Provider;
            if (WebSecurity.UserExists("admin") == false)
            {
                WebSecurity.CreateUserAndAccount("admin", "changethis!", new { Email = "tzonov_@abv.bg" });
            }
            if (roleProvider.RoleExists("administrator") == false)
            {
                roleProvider.CreateRole("administrator");
            }
            if (roleProvider.IsUserInRole("admin", "administrator") == false)
            {
                roleProvider.AddUsersToRoles(new string[] { "admin" }, new string[] { "administrator" });
            }
        }
    }
}
