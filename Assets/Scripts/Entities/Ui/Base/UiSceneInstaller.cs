using Mario.Services;
using Mario.Zenject.Extensions;
using Mario.Zenject.Scene;
using UnityEngine;
using Zenject;

namespace Mario.Entities.Ui.Base
{
    public class UiSceneInstaller : MonoInstaller
    {
        [SerializeField] private UiProvider uiProvider;
        
        public override void InstallBindings()
        {
            Container.BindEntity(uiProvider);
            
            Container
                .BindFactory<UiType, Ui, UiFacade>()
                .FromSubContainerResolve()
                .ByNewGameObjectInstaller<UiInstaller>();
            
            Container.BindInterfaces<UiService>();

        }
    }
}