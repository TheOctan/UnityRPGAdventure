using System;
using System.Collections.Generic;
using OctanGames.Infrastructure.Services;
using OctanGames.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace OctanGames.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        event Action HeroCreated;
        GameObject HeroGameObject { get; }
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgressWriter> ProgressWriters { get; }
        GameObject CreateHero(GameObject initialPoint);
        GameObject CreateHud();
        void Cleanup();
        void Register(ISavedProgressReader progressReader);
    }
}