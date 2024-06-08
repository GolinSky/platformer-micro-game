using System.Collections.Generic;
using LightWeightFramework.Components.Service;
using Mario.Entities.Ui.Base;

namespace Mario.Services
{
    public interface IUiService : IService
    {
        void Show(UiType uiType);
        void Close(UiType uiType);
    }
    
    public class UiService: Service, IUiService
    {
        private readonly UiFacade facade;

        private Dictionary<UiType, Ui> uiWindows = new Dictionary<UiType, Ui>();

        public UiService(UiFacade facade)
        {
            this.facade = facade;
        }

        public void Show(UiType uiType)
        {
            GetOrConstructUi(uiType).Show();
        }

        public void Close(UiType uiType)
        {
            GetOrConstructUi(uiType).Close();
        }

        private Ui GetOrConstructUi(UiType uiType)
        {
            if (uiWindows.TryGetValue(uiType, out Ui targetUi))
            {
                return targetUi;
            }

            Ui ui = facade.Create(uiType);
            uiWindows.Add(uiType, ui);
            return ui;
        }
    }
}