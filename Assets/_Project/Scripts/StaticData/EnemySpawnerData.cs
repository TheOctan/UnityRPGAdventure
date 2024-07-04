using System;
using UnityEngine;

namespace OctanGames.StaticData
{
    [Serializable]
    public class EnemySpawnerData
    {
        public string Id;
        public MonsterType MonsterType;
        public Vector3 Position;

        public EnemySpawnerData(string id, MonsterType monsterType, Vector3 position)
        {
            Id = id;
            MonsterType = monsterType;
            Position = position;
        }
    }
}