using OctanGames.Data;
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

        private void Awake()
        {
            _id = GetComponent<UniqueId>().Id;
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
            
        }
    }
}