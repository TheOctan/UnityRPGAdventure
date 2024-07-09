using UnityEngine;

namespace OctanGames.StaticData
{
    [CreateAssetMenu(fileName = "MonsterData", menuName = "Static Data/Monster", order = 0)]
    public class MonsterStaticData : ScriptableObject
    {
        public MonsterType MonsterTypeId;

        [Range(1, 100)]
        public int Hp;
        [Range(1f, 30f)]
        public float Damage;

        [Min(0)]
        public int MinLoot;
        [Min(0)]
        public int MaxLoot;

        [Range(0,10)]
        public float MoveSpeed = 3;
        [Range(0.5f, 1f)]
        public float EffectiveDistance = 0.6f;
        [Range(0.5f, 1f)]
        public float Cleavage;

        public GameObject Prefab;
    }
}