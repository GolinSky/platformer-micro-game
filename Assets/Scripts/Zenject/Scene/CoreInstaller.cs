using LightWeightFramework.Components.Repository;
using Mario.Entities.Player;
using Mario.Services;
using Mario.Zenject.Extensions;
using Mario.Zenject.Scene;
using UnityEngine;
using Zenject;

public class CoreInstaller : MonoInstaller
{
    [SerializeField] private Transform spawnPoint;
    
    [Inject] private IRepository Repository { get; }

    public override void InstallBindings()
    {
        Container
            .BindFactory<Vector3, PlayerView, PlayerFacade>()
            .FromSubContainerResolve()
            .ByNewContextPrefab<PlayerInstaller>(Repository.LoadComponent<PlayerInstaller>(nameof(PlayerInstaller)));

        Container
            .BindInterfacesNonLazy<CoreService>();

        Container
            .BindInstance(spawnPoint)
            .WithId(TransformInjectKeys.SpawnPoint)
            .AsSingle();
        
    }

}