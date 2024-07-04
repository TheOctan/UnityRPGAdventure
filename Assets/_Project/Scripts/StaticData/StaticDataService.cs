using System.Collections.Generic;
using System.Linq;
using OctanGames.Services;
using UnityEngine;

namespace OctanGames.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string STATIC_DATA_MONSTERS = "StaticData/Monsters";
        private const string STATIC_DATA_LEVELS = "StaticData/Levels";

        private Dictionary<MonsterType, MonsterStaticData> _monsters;
        private Dictionary<string, LevelStaticData> _levels;

        public void LoadMonsters()
        {
            _monsters = Resources
                .LoadAll<MonsterStaticData>(STATIC_DATA_MONSTERS)
                .ToDictionary(e => e.MonsterTypeId, e => e);
            
            _levels = Resources
                .LoadAll<LevelStaticData>(STATIC_DATA_LEVELS)
                .ToDictionary(e => e.LevelKey, e => e);
        }

        public MonsterStaticData ForMonster(MonsterType type) =>
            _monsters.TryGetValue(type, out MonsterStaticData staticData)
                ? staticData
                : null;

        public LevelStaticData ForLevel(string sceneKey) =>
            _levels.TryGetValue(sceneKey, out LevelStaticData staticData)
                ? staticData
                : null;
    }
}