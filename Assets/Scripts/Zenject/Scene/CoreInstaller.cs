using LightWeightFramework.Components.Repository;
using Mario.Entities.Player;
using Mario.Entities.PositionProvider;
using Mario.Services;
using Mario.Services.TokenService;
using Mario.Zenject.Extensions;
using Mario.Zenject.Scene;
using UnityEngine;
using Zenject;

public class CoreInstaller : MonoInstaller
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private PositionProvider positionProvider;
    
    [Inject] private IRepository Repository { get; }

    public override void InstallBindings()
    {
        Container
            .BindFactory<Vector3, PlayerView, PlayerFacade>()
            .FromSubContainerResolve()
            .ByNewContextPrefab<PlayerInstaller>(Repository.LoadComponent<PlayerInstaller>(nameof(PlayerInstaller)));

        Container
            .BindInterfacesNonLazy<CoreService>()
            .BindInterfaces<InputService>()
            .BindInterfaces<TokenService>();

        Container
            .BindInstance(spawnPoint)
            .WithId(TransformInjectKeys.SpawnPoint)
            .AsSingle();

        Container.BindEntity(positionProvider);
        
    }
}