using OctanGames.Data;
using OctanGames.Infrastructure.Factory;
using OctanGames.Services;
using UnityEngine;

namespace OctanGames.Enemy
{
    public class LootSpawner : MonoBehaviour
    {
        [SerializeField] private EnemyDeath _enemyDeath;

        private IGameFactory _factory;
        private IRandomService _random;

        private int _lootMin;
        private int _lootMax;

        public void Construct(IGameFactory factory, IRandomService random)
        {
            _factory = factory;
            _random = random;
        }
        
        private void Start()
        {
            _enemyDeath.Died += SpawnLoot;
        }

        public void SetLoot(int min, int max)
        {
            _lootMin = min;
            _lootMax = max;
        }

        private void SpawnLoot()
        {
            GameObject loot = _factory.CreateLoot();
            loot.transform.position = transform.position;

            var lootItem = new Loot()
            {
                Value = _random.Next(_lootMin, _lootMax)
            };
            loot.GetComponent<LootPiece>().Initialize(lootItem);
        }
    }
}