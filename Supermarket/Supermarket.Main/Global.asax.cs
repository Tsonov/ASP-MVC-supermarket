using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using StructureMap;
using Supermarket.Core.Repositories;
using Supermarket.Main.DataInfrastructure;
using WebMatrix.WebData;

namespace Supermarket.Main
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            MapperConfig.RegisterMaps();
            //Used for membership
            WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
        }

        private static void InitStructureMap()
        {

            ObjectFactory.Initialize(x =>
            {
                x.For<IUsersRepository>().HttpContextScoped().Use<UsersRepository>();
                x.For<ISupermarketItemsRepository>().HttpContextScoped().Use<SupermarketItemsRepository>();
                x.For<IReplenishmentRepository>().HttpContextScoped().Use<ReplenishmentRepository>();
                x.For<IReportsRepository>().HttpContextScoped().Use<ReportsRepository>();
                x.For<ISalesRepository>().HttpContextScoped().Use<SalesRepository>();
            });
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            ObjectFactory.ReleaseAndDisposeAllHttpScopedObjects();
        }
    }
}