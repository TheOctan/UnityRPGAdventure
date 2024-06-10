using OctanGames.Data;
using UnityEngine;

namespace OctanGames.Infrastructure.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string PROGRESS_KEY = "Progress";

        public void SaveProgress()
        {
            throw new System.NotImplementedException();
        }

        public PlayerProgress LoadProgress() => PlayerPrefs.GetString(PROGRESS_KEY)?.ToDeserialized<PlayerProgress>();
    }
}