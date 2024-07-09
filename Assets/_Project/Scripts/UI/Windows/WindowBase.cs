using UnityEngine;
using UnityEngine.UI;

namespace OctanGames.UI.Windows
{
    public abstract class WindowBase : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;

        protected virtual void Awake()
        {
            _closeButton.onClick.AddListener(CloseButtonClicked);
        }

        private void CloseButtonClicked()
        {
            Destroy(gameObject);
        }
    }
}