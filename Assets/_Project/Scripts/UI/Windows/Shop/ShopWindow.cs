using OctanGames.Infrastructure.Services.Ads;
using OctanGames.Infrastructure.Services.PersistentProgress;
using TMPro;
using UnityEngine;

namespace OctanGames.UI.Windows.Shop
{
    public class ShopWindow : WindowBase
    {
        [SerializeField] private TextMeshProUGUI _skullText;
        [SerializeField] private RewardedAdItem _adItem;

        public WindowBase Construct(IAdsService adsService, IPlayerProgressService progressService)
        {
            base.Construct(progressService);
            _adItem.Construct(adsService, progressService);

            return this;
        }
        
        protected override void Initialize()
        {
            _adItem.Initialize();
            RefreshSkullText();
        }

        protected override void SubscribeUpdates()
        {
            _adItem.Subscribe();
            Progress.WorldData.LootData.Changed += RefreshSkullText;
        }

        protected override void Cleanup()
        {
            base.Cleanup();
            _adItem.Cleanup();
            Progress.WorldData.LootData.Changed -= RefreshSkullText;
        }

        private void RefreshSkullText() => _skullText.text = Progress.WorldData.LootData.Collected.ToString();
    }
}