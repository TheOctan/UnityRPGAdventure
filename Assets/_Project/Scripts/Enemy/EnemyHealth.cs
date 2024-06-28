using System;
using OctanGames.Logic;
using UnityEngine;

namespace OctanGames.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        public event Action HealthChanged;

        [Header("Properties")]
        [SerializeField] private float _current;
        [SerializeField] private float _max;
        [Header("Components")]
        [SerializeField] private EnemyAnimator _animator;

        public float Current
        {
            get => _current;
            set => _current = value;
        }

        public float Max
        {
            get => _max;
            set => _max = value;
        }

        public void TakeDamage(float damage)
        {
            Current -= damage;

            _animator.PlayHit();
            HealthChanged?.Invoke();
        }
    }
}