using System;

namespace OctanGames.Data
{
    [Serializable]
    public class PlayerState
    {
        public float CurrentHP;
        public float MaxHP;

        public void ResetHP() => CurrentHP = MaxHP;
    }
}