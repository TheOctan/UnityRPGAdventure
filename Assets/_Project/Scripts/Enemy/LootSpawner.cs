using OctanGames.Data;
using OctanGames.Infrastructure.Factory;
using OctanGames.Logic;
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
            LootPiece lootPiece = _factory.CreateLoot();
            lootPiece.transform.position = transform.position;
            lootPiece.GetComponent<UniqueId>().GenerateId();

            Loot lootItem = GenerateLoot();
            lootPiece.Initialize(lootItem);
        }

        private Loot GenerateLoot()
        {
            return new Loot()
            {
                Value = _random.Next(_lootMin, _lootMax)
            };
        }
    }
}