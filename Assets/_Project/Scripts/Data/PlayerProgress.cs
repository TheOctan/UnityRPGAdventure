using System;

namespace OctanGames.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData;
        public PlayerState PlayerState;
        public Stats PlayerStats;

        public PlayerProgress(string initialLevel)
        {
            WorldData = new WorldData(initialLevel);
            PlayerState = new PlayerState();
            PlayerStats = new Stats();
        }
    }
}