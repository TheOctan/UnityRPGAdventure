using System.Linq;
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
        [SerializeField] private float _cleavage = 0.5f;
        [SerializeField] private float _effectiveDistance = 0.5f;

        [Header("Components")]
        [SerializeField] private EnemyAnimator _animator;

        private IGameFactory _gameFactory;
        private Transform _heroTransform;
        private readonly Collider[] _hits = new Collider[1];

        private float _cooldown;
        private bool _isAttacking;
        private int _layerMask;

        private void Start()
        {
            _gameFactory = ServiceLocator.Container.Single<IGameFactory>();
            _gameFactory.HeroCreated += OnHeroCreated;

            _layerMask = 1 << LayerMask.NameToLayer("Player");
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
            if (Hit(out Collider hit))
            {
                PhysicsDebug.DrawDebug(StartPoint(), _cleavage,1);
            }
        }

        private void OnAttackEnded()
        {
            _cooldown = _attackCooldown;
            _isAttacking = false;
        }

        private void StartAttack()
        {
            transform.LookAt(_heroTransform);
            _animator.PlayAttack();
            _isAttacking = true;
        }

        private bool Hit(out Collider hit)
        {
            Vector3 startPoint = StartPoint();

            int hitCount = Physics.OverlapSphereNonAlloc(startPoint, _cleavage, _hits, _layerMask);
            hit = _hits.FirstOrDefault();

            return hitCount > 0;
        }

        private void UpdateCooldown()
        {
            if (CooldownIsUp()) return;
            _cooldown -= Time.deltaTime;
        }

        private Vector3 StartPoint()
        {
            Transform enemyTransform = transform;
            Vector3 startPoint = enemyTransform.position + Vector3.up * 0.5f +
                                 enemyTransform.forward * _effectiveDistance;
            return startPoint;
        }

        private bool CanAttack() => !_isAttacking && CooldownIsUp();
        private bool CooldownIsUp() => _cooldown <= 0;
        private void OnHeroCreated() => _heroTransform = _gameFactory.HeroGameObject.transform;
    }
}