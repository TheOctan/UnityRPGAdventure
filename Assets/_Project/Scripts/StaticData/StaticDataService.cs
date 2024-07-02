using System.Collections.Generic;
using System.Linq;
using OctanGames.Services;
using UnityEngine;

namespace OctanGames.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string STATIC_DATA_MONSTERS = "StaticData/Monsters";

        private Dictionary<MonsterType, MonsterStaticData> _monsters;

        public void LoadMonsters()
        {
            _monsters = Resources
                .LoadAll<MonsterStaticData>(STATIC_DATA_MONSTERS)
                .ToDictionary(e => e.MonsterTypeId, e => e);
        }

        public MonsterStaticData ForMonster(MonsterType type) =>
            _monsters.TryGetValue(type, out MonsterStaticData staticData)
                ? staticData
                : null;
    }
}