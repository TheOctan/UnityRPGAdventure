using UnityEngine;

namespace OctanGames.StaticData
{
    [CreateAssetMenu(fileName = "MonsterData", menuName = "StaticData/Monster", order = 0)]
    public class MonsterStaticData : ScriptableObject
    {
        public MonsterType MonsterTypeId;

        [Range(1, 100)]
        public int Hp;
        [Range(1f, 30f)]
        public float Damage;

        [Range(0.5f, 1f)]
        public float EffectiveDistance;
        [Range(0.5f, 1f)]
        public float Cleavage;

        public GameObject Prefab;
    }
}