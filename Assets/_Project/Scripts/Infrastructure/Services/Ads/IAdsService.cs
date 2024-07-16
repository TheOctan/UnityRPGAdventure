using System;

namespace OctanGames.Infrastructure.Services.Ads
{
    public interface IAdsService
    {
        event Action InterstitialVideoReady;
        event Action RewardedVideoReady;
        void Initialize(bool testMode = false);
        void LoadInterstitialAd();
        void LoadRewardedAd();
        void ShowInterstitialVideo(Action onVideoFinished);
        void ShowRewardedVideo(Action onVideoFinished);
    }
}