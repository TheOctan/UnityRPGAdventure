using UnityEngine;

namespace OctanGames.Infrastructure
{
    public class GameFactory : IGameFactory
    {
        private const string HERO_PATH = "Hero/Hero";
        private const string HUD_PATH = "Hud/Hud";

        public GameObject CreateHero(GameObject initialPoint)
        {
            return Instantiate(HERO_PATH, position: initialPoint.transform.position);
        }

        public GameObject CreateHud()
        {
            return Instantiate(HUD_PATH);
        }

        private static GameObject Instantiate(string path)
        {
            var heroPrefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(heroPrefab);
        }

        private static GameObject Instantiate(string path, Vector3 position)
        {
            var heroPrefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(heroPrefab, position, Quaternion.identity);
        }
    }
}