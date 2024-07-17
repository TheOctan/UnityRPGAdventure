using OctanGames.Infrastructure.Services.Ads;
using OctanGames.Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.UI;

namespace OctanGames.UI.Windows.Shop
{
    public class RewardedAdItem : MonoBehaviour
    {
        [SerializeField] private Button _showAdButton;
        [SerializeField] private GameObject[] _adActiveObjects;
        [SerializeField] private GameObject[] _adInactiveObjects;

        private IAdsService _adsService;
        private IPlayerProgressService _progressService;

        public RewardedAdItem Construct(IAdsService adsService, IPlayerProgressService progressService)
        {
            _adsService = adsService;
            _progressService = progressService;

            return this;
        }
        public void Initialize()
        {
            _showAdButton.onClick.AddListener(OnShowButtonClicked);
            RefreshAvailableAd(_adsService.IsRewardReady);
        }

        public void Subscribe() => _adsService.RewardedVideoReady += OnRewardedVideoReady;
        public void Cleanup() => _adsService.RewardedVideoReady -= OnRewardedVideoReady;

        private void OnShowButtonClicked()
        {
            _adsService.ShowRewardedVideo(OnVideoFinished);
            RefreshAvailableAd(false);
        }

        private void OnVideoFinished() => _progressService.Progress.WorldData.LootData.Add(_adsService.Reward);

        private void RefreshAvailableAd(bool isActive)
        {
            foreach (GameObject adActiveObject in _adActiveObjects)
            {
                adActiveObject.SetActive(isActive);
            }

            foreach (GameObject adInactiveObject in _adInactiveObjects)
            {
                adInactiveObject.SetActive(!isActive);
            }
        }

        private void OnRewardedVideoReady() => RefreshAvailableAd(true);
    }
}