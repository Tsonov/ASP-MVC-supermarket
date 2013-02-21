using StructureMap;
using Supermarket.Core.Repositories;
using Supermarket.Main.DataInfrastructure;
namespace Supermarket.Main {
    public static class IoC {
        public static IContainer Initialize() {
            ObjectFactory.Initialize(x =>
                        {
                            x.Scan(scan =>
                                    {
                                        scan.TheCallingAssembly();
                                        scan.WithDefaultConventions();
                                    });
                        });
            return ObjectFactory.Container;
        }


    }
}