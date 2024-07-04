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
    }
}