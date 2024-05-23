using OctanGames.Infrastructure.Services;
using UnityEngine;

namespace OctanGames.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateHero(GameObject initialPoint);
        GameObject CreateHud();
    }
}