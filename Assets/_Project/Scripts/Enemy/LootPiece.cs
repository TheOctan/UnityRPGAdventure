using System;
using System.Collections;
using OctanGames.Data;
using OctanGames.Infrastructure.Services.PersistentProgress;
using OctanGames.Logic;
using TMPro;
using UnityEngine;

namespace OctanGames.Enemy
{
    public class LootPiece : MonoBehaviour, ISavedProgressWriter
    {
        private const float DELAY_BEFORE_DESTROY = 1.5f;

        [SerializeField] private GameObject _skull;
        [SerializeField] private GameObject _pickupFxPrefab;
        [SerializeField] private TextMeshPro _lootText;
        [SerializeField] private GameObject _pickupPopup;

        private WorldData _worldData;
        private Loot _loot;
        private string _id;
        private bool _picked;

        public void Construct(WorldData worldData) => _worldData = worldData;
        public void Initialize(Loot loot) => _loot = loot;
        private void Start() => _id = GetComponent<UniqueId>().Id;

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

        private void UpdateWorldData()
        {
            UpdateCollectedLootAmount();
            RemoveLootPieceFromSavedPieces();
        }

        private void UpdateCollectedLootAmount() => _worldData.LootData.Collect(_loot);

        private void RemoveLootPieceFromSavedPieces()
        {
            LootPieceDataDictionary savedLootPieces = _worldData.LootData.LootPiecesOnScene;

            if (!savedLootPieces.Dictionary.ContainsKey(_id)) return;
            savedLootPieces.Dictionary.Remove(_id);
        }

        private void HideSkull() => _skull.SetActive(false);
        private void PlayPickupFx() => Instantiate(_pickupFxPrefab, transform.position, Quaternion.identity);

        private void ShowText()
        {
            _lootText.text = _loot.Value.ToString();
            _pickupPopup.SetActive(true);
        }

        private IEnumerator StartDestroyTimer()
        {
            yield return new WaitForSeconds(DELAY_BEFORE_DESTROY);
            Destroy(gameObject);
        }

        void ISavedProgressWriter.SaveProgress(PlayerProgress progress)
        {
            if (_picked) return;
            LootPieceDataDictionary lootPiecesOnScene = progress.WorldData.LootData.LootPiecesOnScene;

            if (lootPiecesOnScene.Dictionary.ContainsKey(_id)) return;
            lootPiecesOnScene.Dictionary.Add(_id, new LootPieceData(transform.position.AsVectorData(), _loot));
        }

        void ISavedProgressReader.LoadProgress(PlayerProgress progress)
        {
        }
    }
}