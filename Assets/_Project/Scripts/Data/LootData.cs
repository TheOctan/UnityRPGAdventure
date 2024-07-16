using System;

namespace OctanGames.Data
{
    [Serializable]
    public class LootData
    {
        public event Action Changed;

        public int Collected;

        public void Collect(Loot loot)
        {
            Collected += loot.Value;
            Changed?.Invoke();
        }

        public void Add(int loot)
        {
            Collected += loot;
            Changed?.Invoke();
        }
    }
}