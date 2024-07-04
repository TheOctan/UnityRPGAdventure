using System.Collections.Generic;
using System.Linq;
using OctanGames.Data;
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

        public MonsterStaticData ForMonster(MonsterType type) => _monsters.GetValueOrNull(type);
        public LevelStaticData ForLevel(string sceneKey) => _levels.GetValueOrNull(sceneKey);
    }
}