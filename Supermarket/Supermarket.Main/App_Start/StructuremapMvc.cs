using System.Web.Mvc;
using StructureMap;
using Supermarket.Main.DependencyResolution;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Supermarket.Main.App_Start.StructuremapMvc), "Start")]

namespace Supermarket.Main.App_Start {
    public static class StructuremapMvc {
        public static void Start() {
            var container = (IContainer) IoC.Initialize();
            DependencyResolver.SetResolver(new SmDependencyResolver(container));
        }
    }
}