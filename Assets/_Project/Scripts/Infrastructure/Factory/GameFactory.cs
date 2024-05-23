using OctanGames.Infrastructure.AssetManagement;
using UnityEngine;

namespace OctanGames.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;

        public GameFactory(IAssetProvider assets)
        {
            _assets = assets;
        }

        public GameObject CreateHero(GameObject initialPoint)
        {
            return _assets.Instantiate(AssetPath.HERO_PATH, position: initialPoint.transform.position);
        }

        public GameObject CreateHud()
        {
            return _assets.Instantiate(AssetPath.HUD_PATH);
        }
    }
}