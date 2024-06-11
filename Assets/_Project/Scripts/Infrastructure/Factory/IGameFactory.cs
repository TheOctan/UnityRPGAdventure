using OctanGames.Infrastructure.Services;
using OctanGames.Infrastructure.Services.PersistentProgress;
using System.Collections.Generic;
using UnityEngine;

namespace OctanGames.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateHero(GameObject initialPoint);
        GameObject CreateHud();
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgressWriter> ProgressWriters { get; }
        void Cleanup();
    }
}