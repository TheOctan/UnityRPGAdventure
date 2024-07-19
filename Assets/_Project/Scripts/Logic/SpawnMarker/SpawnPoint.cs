using OctanGames.Data;
using OctanGames.Enemy;
using OctanGames.Infrastructure.Factory;
using OctanGames.Infrastructure.Services.PersistentProgress;
using OctanGames.StaticData;
using UnityEngine;

namespace OctanGames.Logic.SpawnMarker
{
    public class SpawnPoint : MonoBehaviour, ISavedProgressWriter
    {
        public MonsterType MonsterTypeId;
        [SerializeField] private bool _slain;
        public string Id { get; set; }

        private IGameFactory _gameFactory;
        private EnemyDeath _enemyDeath;

        public SpawnPoint Construct(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
            return this;
        }

        private void Slay()
        {
            _slain = true;
            if (_enemyDeath == null) return;
            _enemyDeath.Died -= Slay;
        }

        private async void Spawn()
        {
            GameObject monster = await _gameFactory.CreateMonster(MonsterTypeId, transform);
            _enemyDeath = monster.GetComponent<EnemyDeath>();
            _enemyDeath.Died += Slay;
        }

        void ISavedProgressReader.LoadProgress(PlayerProgress progress)
        {
            if (progress.KillData.ClearedSpawners.Contains(Id))
            {
                _slain = true;
            }
            else
            {
                Spawn();
            }
        }

        void ISavedProgressWriter.SaveProgress(PlayerProgress progress)
        {
            if (!_slain) return;
            progress.KillData.ClearedSpawners.Add(Id);
        }
    }
}