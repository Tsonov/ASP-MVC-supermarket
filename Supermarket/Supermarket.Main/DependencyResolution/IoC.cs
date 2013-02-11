using StructureMap;
using Supermarket.Core;
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
                           x.For<IUsersRepository>().HttpContextScoped().Use<UsersContext>();
                        });
            return ObjectFactory.Container;
        }
    }
}