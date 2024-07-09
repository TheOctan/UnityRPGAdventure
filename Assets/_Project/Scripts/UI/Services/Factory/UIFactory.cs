using OctanGames.Infrastructure.AssetManagement;
using OctanGames.Infrastructure.Services.PersistentProgress;
using OctanGames.Services;
using OctanGames.StaticData.Windows;
using OctanGames.UI.Services.Windows;
using OctanGames.UI.Windows;
using UnityEngine;

namespace OctanGames.UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;
        private readonly IPlayerProgressService _progressService;

        private Transform _uiRoot;

        public UIFactory(IAssetProvider assets,
            IStaticDataService staticData,
            IPlayerProgressService progressService)
        {
            _assets = assets;
            _staticData = staticData;
            _progressService = progressService;
        }

        public void CreateShop()
        {
            WindowConfig config = _staticData.ForWindow(WindowType.Shop);
            WindowBase window = Object.Instantiate(config.Prefab, _uiRoot);
            window.Construct(_progressService);
        }

        public void CreateUIRoot() => _uiRoot = _assets.Instantiate(AssetPath.UI_ROOT).transform;
    }
}