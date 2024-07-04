using OctanGames.Data;
using TMPro;
using UnityEngine;

namespace OctanGames.UI
{
    public class LootCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _counter;

        private WorldData _worldData;

        public void Construct(WorldData worldData)
        {
            _worldData = worldData;
            _worldData.LootData.Changed += OnLootDataChanged;
        }

        private void Start() => UpdateCounter();
        private void OnDestroy() => _worldData.LootData.Changed -= OnLootDataChanged;
        private void OnLootDataChanged() => UpdateCounter();
        private void UpdateCounter() => _counter.text = _worldData.LootData.Collected.ToString();
    }
}