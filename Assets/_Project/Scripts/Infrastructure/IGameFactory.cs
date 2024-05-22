using UnityEngine;

namespace OctanGames.Infrastructure
{
    public interface IGameFactory
    {
        GameObject CreateHero(GameObject initialPoint);
        GameObject CreateHud();
    }
}