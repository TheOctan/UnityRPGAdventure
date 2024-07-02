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
        public float EffectiveDistance = 0.6f;
        [Range(0.5f, 1f)]
        public float Cleavage;
        [Range(0,10)]
        public float MoveSpeed = 3;

        public GameObject Prefab;
    }
}