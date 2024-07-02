using System.Linq;
using OctanGames.Logic;
using UnityEngine;

namespace OctanGames.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class Attack : MonoBehaviour
    {
        [Header("Properties")]
        public float AttackCooldown = 3f;
        public float Cleavage = 0.5f;
        public float EffectiveDistance = 0.5f;
        public float Damage = 10f;

        [Header("Components")] [SerializeField]
        private EnemyAnimator _animator;

        private Transform _heroTransform;
        private readonly Collider[] _hits = new Collider[1];

        private float _cooldown;
        private bool _isAttacking;
        private int _layerMask;

        private bool _attackIsActive;

        public void Construct(Transform heroTransform)
        {
            _heroTransform = heroTransform;
        }

        private void Awake() => _layerMask = 1 << LayerMask.NameToLayer("Player");

        private void Update()
        {
            UpdateCooldown();

            if (!CanAttack()) return;
            StartAttack();
        }

        public void EnableAttack() => _attackIsActive = true;
        public void DisableAttack() => _attackIsActive = false;

        private void OnAttack()
        {
            if (!Hit(out Collider hit)) return;

            PhysicsDebug.DrawDebug(StartPoint(), Cleavage, 1);
            if (hit.TryGetComponent(out IHealth heroHealth))
            {
                heroHealth.TakeDamage(Damage);
            }
        }

        private void OnAttackEnded()
        {
            _cooldown = AttackCooldown;
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

            int hitCount = Physics.OverlapSphereNonAlloc(startPoint, Cleavage, _hits, _layerMask);
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
                                 enemyTransform.forward * EffectiveDistance;
            return startPoint;
        }

        private bool CanAttack() => _attackIsActive && !_isAttacking && CooldownIsUp();
        private bool CooldownIsUp() => _cooldown <= 0;
    }
}