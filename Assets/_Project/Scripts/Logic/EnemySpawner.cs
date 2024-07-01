using OctanGames.Data;
using OctanGames.Enemy;
using OctanGames.Infrastructure.Factory;
using OctanGames.Infrastructure.Services;
using OctanGames.Infrastructure.Services.PersistentProgress;
using OctanGames.StaticData;
using UnityEngine;

namespace OctanGames.Logic
{
    public class EnemySpawner : MonoBehaviour, ISavedProgressWriter
    {
        [SerializeField] private MonsterType _monsterTypeId;
        private string _id;

        [SerializeField] private bool _slain;

        private IGameFactory _factory;
        private EnemyDeath _enemyDeath;

        private void Awake()
        {
            _id = GetComponent<UniqueId>().Id;
            _factory = ServiceLocator.Container.Single<IGameFactory>();
        }

        void ISavedProgressReader.LoadProgress(PlayerProgress progress)
        {
            if (progress.KillData.ClearedSpawners.Contains(_id))
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
            progress.KillData.ClearedSpawners.Add(_id);
        }

        private void Spawn()
        {
            GameObject monster = _factory.CreateMonster(_monsterTypeId, transform);
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