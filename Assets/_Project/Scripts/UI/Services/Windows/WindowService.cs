using System;
using OctanGames.UI.Services.Factory;

namespace OctanGames.UI.Services.Windows
{
    public class WindowService : IWindowService
    {
        private readonly IUIFactory _uiFactory;

        public WindowService(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        public void Open(WindowType windowType)
        {
            switch (windowType)
            {
                case WindowType.Unknown:
                    break;
                case WindowType.Shop:
                    _uiFactory.CreateShop();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(windowType), windowType, null);
            }
        }
    }
}