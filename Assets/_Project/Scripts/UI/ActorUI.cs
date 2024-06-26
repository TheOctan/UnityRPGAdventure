using OctanGames.Hero;
using UnityEngine;

namespace OctanGames.UI
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private HpBar _hpBar;

        private HeroHealth _heroHealth;

        public void Construct(HeroHealth heroHealth)
        {
            _heroHealth = heroHealth;
            _heroHealth.HealthChanged += UpdateHpBar;
        }

        private void OnDestroy()
        {
            _heroHealth.HealthChanged -= UpdateHpBar;
        }

        private void UpdateHpBar()
        {
            _hpBar.SetValue(_heroHealth.Current, _heroHealth.Max);
        }
    }
}