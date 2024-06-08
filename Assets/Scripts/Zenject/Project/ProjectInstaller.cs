using LightWeightFramework.Components.Repository;
using Mario.Repository;
using Mario.Services;
using Mario.Services.SceneLoading;
using Mario.Zenject.Extensions;

namespace Zenject.Project
{
    public class ProjectInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesNonLazy<AddressableRepository>()
                .BindInterfaces<SceneService>()
                .BindInterfaces<AudioService>();

            IRepository repository = Container.Resolve<IRepository>();
            Container.BindModel<SceneModel>(repository);
        }
    }
}