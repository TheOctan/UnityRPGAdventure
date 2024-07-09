using OctanGames.Infrastructure.AssetManagement;
using OctanGames.Services;
using OctanGames.StaticData.Windows;
using OctanGames.UI.Services.Windows;
using UnityEngine;

namespace OctanGames.UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;

        private Transform _uiRoot;

        public UIFactory(IAssetProvider assets, IStaticDataService staticData)
        {
            _assets = assets;
            _staticData = staticData;
        }

        public void CreateShop()
        {
            WindowConfig config = _staticData.ForWindow(WindowType.Shop);
            Object.Instantiate(config.Prefab, _uiRoot);
        }

        public void CreateUIRoot() => _uiRoot = _assets.Instantiate(AssetPath.UI_ROOT).transform;
    }
}