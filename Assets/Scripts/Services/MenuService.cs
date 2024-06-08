using LightWeightFramework.Components.Service;
using Mario.Entities.Ui.Base;
using Zenject;

namespace Mario.Services
{
    public class MenuService: Service, IInitializable
    {
        private readonly IUiService uiService;

        public MenuService(IUiService uiService)
        {
            this.uiService = uiService;
        }
        
        public void Initialize()
        {
            uiService.Show(UiType.LobbyMenu);
        }
    }
}