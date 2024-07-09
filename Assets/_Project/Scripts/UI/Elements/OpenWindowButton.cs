using OctanGames.UI.Services.Windows;
using UnityEngine;
using UnityEngine.UI;

namespace OctanGames.UI.Elements
{
    public class OpenWindowButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private WindowType _windowType;
        private IWindowService _windowService;

        public OpenWindowButton Construct(IWindowService windowService)
        {
            _windowService = windowService;
            return this;
        }
        
        private void Awake()
        {
            _button.onClick.AddListener(ButtonClicked);
        }

        private void ButtonClicked() => _windowService.Open(_windowType);
    }
}