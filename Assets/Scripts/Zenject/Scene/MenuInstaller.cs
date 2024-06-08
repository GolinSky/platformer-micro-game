using Mario.Services;
using Mario.Zenject.Extensions;
using Zenject;

namespace Mario.Zenject.Scene
{
    public class MenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesNonLazy<MenuService>();
        }
    }
}