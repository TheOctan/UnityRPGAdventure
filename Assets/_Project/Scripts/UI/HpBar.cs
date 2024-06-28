using UnityEngine;
using UnityEngine.UI;

namespace OctanGames.UI
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] private Image _image;

        public void SetValue(float current, float max) => _image.fillAmount = Mathf.Clamp01(current / max);
    }
}