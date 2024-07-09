using OctanGames.Data;
using OctanGames.Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.UI;

namespace OctanGames.UI.Windows
{
    public abstract class WindowBase : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;

        protected IPlayerProgressService ProgressService;
        protected PlayerProgress Progress => ProgressService.Progress;

        public WindowBase Construct(IPlayerProgressService progressService)
        {
            ProgressService = progressService;

            return this;
        }

        protected virtual void Awake()
        {
            _closeButton.onClick.AddListener(CloseButtonClicked);
        }

        private void Start()
        {
            Initialize();
            SubscribeUpdates();
        }

        private void OnDestroy() => Cleanup();

        protected virtual void Initialize()
        {
        }
        protected virtual void SubscribeUpdates()
        {
        }
        protected virtual void Cleanup()
        {
        }

        private void CloseButtonClicked()
        {
            Destroy(gameObject);
        }
    }
}