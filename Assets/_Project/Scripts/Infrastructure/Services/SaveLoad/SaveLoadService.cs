using OctanGames.Data;
using OctanGames.Infrastructure.Factory;
using OctanGames.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace OctanGames.Infrastructure.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string PROGRESS_KEY = "Progress";

        private readonly IPlayerProgressService _progressService;
        private readonly IGameFactory _gameFactory;

        public SaveLoadService(IPlayerProgressService progressService, IGameFactory gameFactory)
        {
            _progressService = progressService;
            _gameFactory = gameFactory;
        }

        public void SaveProgress()
        {
            foreach (ISavedProgressWriter progressWriter in _gameFactory.ProgressWriters)
            {
                progressWriter.SaveProgress(_progressService.Progress);
            }

            PlayerPrefs.SetString(PROGRESS_KEY, _progressService.Progress.ToJson());
        }

        public PlayerProgress LoadProgress() => PlayerPrefs.GetString(PROGRESS_KEY)?.ToDeserialized<PlayerProgress>();
    }
}