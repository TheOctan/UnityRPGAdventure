using System.Collections;
using OctanGames.Data;
using TMPro;
using UnityEngine;

namespace OctanGames.Enemy
{
    public class LootPiece : MonoBehaviour
    {
        [SerializeField] private GameObject _skull;
        [SerializeField] private GameObject _pickupFxPrefab;
        [SerializeField] private TextMeshPro _lootText;
        [SerializeField] private GameObject _pickupPopup;

        private WorldData _worldData;
        private Loot _loot;
        private bool _picked;

        public void Construct(WorldData worldData) => _worldData = worldData;
        public void Initialize(Loot loot) => _loot = loot;
        private void OnTriggerEnter(Collider other) => Pickup();

        private void Pickup()
        {
            if(_picked) return;
            _picked = true;

            UpdateWorldData();

            HideSkull();
            PlayPickupFx();
            ShowText();

            StartCoroutine(StartDestroyTimer());
        }

        private void UpdateWorldData() => _worldData.LootData.Collect(_loot);
        private void HideSkull() => _skull.SetActive(false);
        private void PlayPickupFx() => Instantiate(_pickupFxPrefab, transform.position, Quaternion.identity);

        private void ShowText()
        {
            _lootText.text = _loot.Value.ToString();
            _pickupPopup.SetActive(true);
        }

        private IEnumerator StartDestroyTimer()
        {
            yield return new WaitForSeconds(1.5f);
            Destroy(gameObject);
        }
    }
}