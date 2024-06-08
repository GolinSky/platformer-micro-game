using LightWeightFramework.Components.Repository;
using Mario.Zenject.Extensions;
using Zenject;

namespace Mario.Entities.Ui.Base
{
    public class UiInstaller: Installer
    {
        private readonly IRepository repository;
        private readonly UiType uiType;
        private readonly IUiProvider uiProvider;


        public UiInstaller(IRepository repository, UiType uiType, UiProvider uiProvider)
        {
            this.repository = repository;
            this.uiType = uiType;
            this.uiProvider = uiProvider;
        }
        
        public override void InstallBindings()
        {
            //Container.BindEntity(uiType);
            Container.BindEntity(uiProvider);
            
            Container
                .BindInterfacesAndSelfTo<Ui>()
                .FromComponentInNewPrefab(repository.LoadComponent<Ui>($"{uiType}{nameof(Ui)}"))
                .UnderTransform(uiProvider.Root)
                .AsSingle();

        }
    }
}