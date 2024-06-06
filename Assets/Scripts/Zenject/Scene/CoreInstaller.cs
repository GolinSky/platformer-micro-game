using LightWeightFramework.Components.Repository;
using Mario.Entities.Player;
using Mario.Services;
using Mario.Zenject.Extensions;
using Zenject;

public class CoreInstaller : MonoInstaller
{
    [Inject] private IRepository Repository { get; }

    public override void InstallBindings()
    {
        Container
            .BindFactory<PlayerView, PlayerFacade>()
            .FromSubContainerResolve()
            .ByNewContextPrefab(Repository.LoadComponent<PlayerInstaller>(nameof(PlayerInstaller)));

        Container
            .BindInterfacesNonLazy<CoreService>();
    }
}