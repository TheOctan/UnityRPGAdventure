using OctanGames.Logic;
using UnityEngine;

namespace OctanGames.UI
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private HpBar _hpBar;

        private IHealth _heroHealth;

        public void Construct(IHealth heroHealth)
        {
            _heroHealth = heroHealth;
            _heroHealth.HealthChanged += UpdateHpBar;
        }

        private void Awake()
        {
            var health = GetComponent<IHealth>();
            if (health == null) return;

            Construct(health);
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