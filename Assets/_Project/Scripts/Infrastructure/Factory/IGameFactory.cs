using System.Collections.Generic;
using System.Threading.Tasks;
using OctanGames.Enemy;
using OctanGames.Infrastructure.Services;
using OctanGames.Infrastructure.Services.PersistentProgress;
using OctanGames.StaticData;
using UnityEngine;

namespace OctanGames.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgressWriter> ProgressWriters { get; }
        GameObject CreateHero(Vector3 initialPoint);
        GameObject CreateHud();
        Task<GameObject> CreateMonster(MonsterType type, Transform parent);
        LootPiece CreateLoot();
        void CreateSpawner(Vector3 position, string spawnerId, MonsterType monsterType);
        void Cleanup();
    }
}