using OctanGames.Data;
using OctanGames.Enemy;
using OctanGames.Infrastructure.Factory;
using OctanGames.Infrastructure.Services;
using OctanGames.Infrastructure.Services.PersistentProgress;
using OctanGames.StaticData;
using UnityEngine;
using UnityEngine.Serialization;

namespace OctanGames.Logic
{
    public class EnemySpawner : MonoBehaviour, ISavedProgressWriter
    {
        [FormerlySerializedAs("_monsterTypeId")] public MonsterType MonsterTypeId;
        [FormerlySerializedAs("_id")] public string Id;
        [SerializeField] private bool _slain;

        private IGameFactory _factory;
        private EnemyDeath _enemyDeath;

        private void Awake()
        {
            Id = GetComponent<UniqueId>().Id;
            _factory = ServiceLocator.Container.Single<IGameFactory>();
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

        private void Spawn()
        {
            GameObject monster = _factory.CreateMonster(MonsterTypeId, transform);
            _enemyDeath = monster.GetComponent<EnemyDeath>();
            _enemyDeath.Died += Slay;
        }

        private void Slay()
        {
            _slain = true;
            if (_enemyDeath == null) return;
            _enemyDeath.Died -= Slay;
        }
    }
}