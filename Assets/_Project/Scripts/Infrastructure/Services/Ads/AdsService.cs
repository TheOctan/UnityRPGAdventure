using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace OctanGames.Infrastructure.Services.Ads
{
    public class AdsService : IAdsService, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        private const string ANDROID_GAME_ID = "5658129";
        private const string INTERSTITIAL_ANDROID_PLACEMENT_ID = "Interstitial_Android";
        private const string REWARDED_ANDROID_PLACEMENT_ID = "Rewarded_Android";
        private const string BANNER_ANDROID_PLACEMENT_ID = "Banner_Android";

        private const string IOS_GAME_ID = "5658128";
        private const string INTERSTITIAL_IOS_PLACEMENT_ID = "Interstitial_iOS";
        private const string REWARDED_IOS_PLACEMENT_ID = "Rewarded_iOS";
        private const string BANNER_IOS_PLACEMENT_ID = "Banner_iOS";

        private const int REWARD_VALUE = 10;

        public event Action InterstitialVideoReady;
        public event Action RewardedVideoReady;

        private Action _onVideoFinished;

        private string _gameId;
        private string _interstitialId;
        private string _rewardedId;
        //private string _bannerId;

        private bool _testMode;

        public int Reward => REWARD_VALUE;

        public void Initialize(bool testMode = false)
        {
            _testMode = testMode;

#if UNITY_IOS
            _gameId = IOS_GAME_ID;
            _interstitialId = INTERSTITIAL_IOS_PLACEMENT_ID;
            _rewardedId = REWARDED_IOS_PLACEMENT_ID;
            //_bannerId = BANNER_IOS_PLACEMENT_ID;
#elif UNITY_ANDROID || UNITY_EDITOR
            _gameId = ANDROID_GAME_ID;
            _interstitialId = INTERSTITIAL_ANDROID_PLACEMENT_ID;
            _rewardedId = REWARDED_ANDROID_PLACEMENT_ID;
            //_bannerId = BANNER_ANDROID_PLACEMENT_ID;
#endif

            if (!Advertisement.isInitialized && Advertisement.isSupported)
            {
                Advertisement.Initialize(_gameId, _testMode, this);
            }
        }

        public void LoadInterstitialAd()
        {
            Advertisement.Load(_interstitialId, this);
        }

        public void LoadRewardedAd()
        {
            Advertisement.Load(_rewardedId, this);
        }

        public void ShowInterstitialVideo(Action onVideoFinished)
        {
            _onVideoFinished = onVideoFinished;
            Advertisement.Show(_interstitialId, this);
            LoadInterstitialAd();
        }

        public void ShowRewardedVideo(Action onVideoFinished)
        {
            _onVideoFinished = onVideoFinished;
            Advertisement.Show(_rewardedId, this);
            LoadRewardedAd();
        }

        void IUnityAdsInitializationListener.OnInitializationComplete() =>
            Debug.Log("Unity Ads initialization complete.");

        void IUnityAdsInitializationListener.
            OnInitializationFailed(UnityAdsInitializationError error, string message) =>
            Debug.Log($"Unity Ads Initialization Failed: {error} - {message}");

        void IUnityAdsLoadListener.OnUnityAdsAdLoaded(string placementId)
        {
            Debug.Log($"{nameof(IUnityAdsLoadListener.OnUnityAdsAdLoaded)} {placementId}");

            if (placementId == _rewardedId)
            {
                RewardedVideoReady?.Invoke();
            }
            else if(placementId == _interstitialId)
            {
                InterstitialVideoReady?.Invoke();
            }
        }

        void IUnityAdsLoadListener.
            OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message) =>
            Debug.Log(
                $"{nameof(IUnityAdsLoadListener.OnUnityAdsFailedToLoad)} {nameof(placementId)}={placementId} {error} - {message}");

        void IUnityAdsShowListener.OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message) =>
            Debug.Log(
                $"{nameof(IUnityAdsShowListener.OnUnityAdsShowFailure)} {nameof(placementId)}={placementId} {error} - {message}");

        void IUnityAdsShowListener.OnUnityAdsShowStart(string placementId) =>
            Debug.Log($"{nameof(IUnityAdsShowListener.OnUnityAdsShowStart)} {nameof(placementId)}={placementId}");

        void IUnityAdsShowListener.OnUnityAdsShowClick(string placementId) =>
            Debug.Log($"{nameof(IUnityAdsShowListener.OnUnityAdsShowClick)} {nameof(placementId)}={placementId}");

        void IUnityAdsShowListener.OnUnityAdsShowComplete(string placementId,
            UnityAdsShowCompletionState showCompletionState)
        {
            var message =
                $"{nameof(IUnityAdsShowListener.OnUnityAdsShowComplete)} {nameof(placementId)}={placementId} {nameof(showCompletionState)}={showCompletionState}";

            switch (showCompletionState)
            {
                case UnityAdsShowCompletionState.SKIPPED:
                    Debug.Log(message);
                    break;
                case UnityAdsShowCompletionState.COMPLETED:
                    _onVideoFinished?.Invoke();
                    break;
                case UnityAdsShowCompletionState.UNKNOWN:
                    Debug.LogError(message);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(showCompletionState), showCompletionState, null);
            }

            _onVideoFinished = null;
        }
    }
}