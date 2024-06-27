using UnityEngine;

namespace OctanGames.Hero
{
    [RequireComponent(typeof(HeroHealth))]
    public class HeroDeath : MonoBehaviour
    {
        [SerializeField] private HeroHealth _health;
        [SerializeField] private HeroMove _heroMove;
        [SerializeField] private HeroAnimator _heroAnimator;
        [SerializeField] private HeroAttack _heroAttack;
        [SerializeField] private GameObject _deathFX;

        private bool _isDead;

        private void Start() => _health.HealthChanged += OnHealthChanged;
        private void OnDestroy() => _health.HealthChanged -= OnHealthChanged;

        private void OnHealthChanged()
        {
            if (_isDead || _health.Current > 0) return;

            Die();
        }

        private void Die()
        {
            _isDead = true;
            
            _heroMove.enabled = false;
            _heroAttack.enabled = false;
            _heroAnimator.PlayDeath();

            SpawnDeathFx();
        }

        private void SpawnDeathFx() => Instantiate(_deathFX, transform.position, Quaternion.identity);
    }
}