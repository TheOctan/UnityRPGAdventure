using System;
using OctanGames.Infrastructure.Factory;
using OctanGames.Infrastructure.Services;
using UnityEngine;

namespace OctanGames.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class Attack : MonoBehaviour
    {
        [Header("Properties")]
        [SerializeField] private float _attackCooldown = 3f;
        [Header("Components")]
        [SerializeField] private EnemyAnimator _animator;

        private IGameFactory _gameFactory;
        private Transform _heroTransform;
        private float _cooldown;
        private bool _isAttacking;

        private void Start()
        {
            _gameFactory = ServiceLocator.Container.Single<IGameFactory>();
            _gameFactory.HeroCreated += OnHeroCreated;
        }

        private void Update()
        {
            UpdateCooldown();

            if (CanAttack())
            {
                StartAttack();
            }
        }

        private void OnAttack()
        {
        }

        private void OnAttackEnded()
        {
            _cooldown = _attackCooldown;
            _isAttacking = false;
        }

        private void UpdateCooldown()
        {
            if (CooldownIsUp()) return;
            _cooldown -= Time.deltaTime;
        }

        private void StartAttack()
        {
            transform.LookAt(_heroTransform);
            _animator.PlayAttack();
            _isAttacking = true;
        }

        private bool CanAttack() => !_isAttacking && CooldownIsUp();
        private bool CooldownIsUp() => _cooldown <= 0;
        private void OnHeroCreated() => _heroTransform = _gameFactory.HeroGameObject.transform;
    }
}