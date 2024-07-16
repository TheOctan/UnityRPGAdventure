using System;

namespace OctanGames.Infrastructure.Services.Ads
{
    public interface IAdsService : IService
    {
        event Action InterstitialVideoReady;
        event Action RewardedVideoReady;
        int Reward { get; }
        void Initialize(bool testMode = false);
        void LoadInterstitialAd();
        void LoadRewardedAd();
        void ShowInterstitialVideo(Action onVideoFinished);
        void ShowRewardedVideo(Action onVideoFinished);
    }
}