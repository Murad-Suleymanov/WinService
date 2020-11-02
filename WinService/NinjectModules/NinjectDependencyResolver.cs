using Ninject.Modules;
using WinService.Abstraction;
using WinService.Configuration;
using WinService.Logic;

namespace WinService.NinjectModules
{
    public class NinjectDependencyResolver : NinjectModule
    {
        public override void Load()
        {
            Bind<ISynchronizer>().To<Synchronizer>();
            Bind<IServiceConfigManager>().To<ServiceConfigManager>();
        }
    }
}
