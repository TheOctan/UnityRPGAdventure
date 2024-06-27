using System;
using UnityEngine;

namespace OctanGames.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyHealth : MonoBehaviour
    {
        public event Action HealthChanged;

        [Header("Properties")]
        [SerializeField] private float _current;
        [SerializeField] private float _max;
        [Header("Components")]
        [SerializeField] private EnemyAnimator _animator;

        public void TakeDamage(float damage)
        {
            _current -= damage;

            _animator.PlayHit();
            HealthChanged?.Invoke();
        }
    }
}