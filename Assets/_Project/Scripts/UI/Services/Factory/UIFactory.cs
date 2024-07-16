using OctanGames.Infrastructure.Services.PersistentProgress;
using OctanGames.Infrastructure.AssetManagement;
using OctanGames.Infrastructure.Services.Ads;
using OctanGames.StaticData.Windows;
using OctanGames.UI.Services.Windows;
using OctanGames.UI.Windows.Shop;
using OctanGames.Services;
using UnityEngine;

namespace OctanGames.UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;
        private readonly IPlayerProgressService _progressService;
        private readonly IAdsService _adsService;

        private Transform _uiRoot;

        public UIFactory(IAssetProvider assets,
            IStaticDataService staticData,
            IPlayerProgressService progressService,
            IAdsService adsService)
        {
            _assets = assets;
            _staticData = staticData;
            _progressService = progressService;
            _adsService = adsService;
        }

        public void CreateShop()
        {
            WindowConfig config = _staticData.ForWindow(WindowType.Shop);
            var window = (ShopWindow)Object.Instantiate(config.Prefab, _uiRoot);
            window.Construct(_adsService, _progressService);
        }

        public void CreateUIRoot() => _uiRoot = _assets.Instantiate(AssetPath.UI_ROOT).transform;
    }
}