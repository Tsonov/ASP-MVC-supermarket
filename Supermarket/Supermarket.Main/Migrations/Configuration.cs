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

    internal sealed class Configuration : DbMigrationsConfiguration<Supermarket.Main.DataInfrastructure.SupermarketDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Supermarket.Main.DataInfrastructure.SupermarketDB context)
        {
            context.Categories.AddOrUpdate(d => d.Name,
                new Category { Name = "Beverages" },
                new Category { Name = "Food" });

            context.CashDesk.AddOrUpdate(d => d.AvailableAmount,
                new CashDesk { AvailableAmount = 1000 });

            //TODO export this ?
            WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);

            var roleProvider = (SimpleRoleProvider)Roles.Provider;
            if(WebSecurity.UserExists("admin") == false)
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
