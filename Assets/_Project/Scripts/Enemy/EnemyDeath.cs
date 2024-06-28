using System;
using System.Collections;
using UnityEngine;

namespace OctanGames.Enemy
{
    [RequireComponent(typeof(EnemyHealth))]
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyDeath : MonoBehaviour
    {
        public event Action Died;

        [SerializeField] private EnemyHealth _health;
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private GameObject _deathFX;

        private void Start() => _health.HealthChanged += OnHealthChanged;
        private void OnDestroy() => _health.HealthChanged -= OnHealthChanged;

        private void OnHealthChanged()
        {
            if (_health.Current > 0) return;

            Die();
        }

        private void Die()
        {
            _health.HealthChanged -= OnHealthChanged;
            _animator.PlayDeath();

            SpawnDeathFx();
            StartCoroutine(DestroyTimer());

            Died?.Invoke();
        }

        private void SpawnDeathFx() => Instantiate(_deathFX, transform.position, Quaternion.identity);

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(3);
            Destroy(gameObject);
        }
    }
}