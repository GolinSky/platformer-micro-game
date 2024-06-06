using Mario.Repository;
using Mario.Zenject.Extensions;

namespace Zenject.Project
{
    public class ProjectInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesNonLazy<AddressableRepository>();
        }
    }
}