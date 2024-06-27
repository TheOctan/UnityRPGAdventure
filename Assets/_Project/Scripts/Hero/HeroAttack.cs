using JetBrains.Annotations;
using OctanGames.Data;
using OctanGames.Infrastructure.Services;
using OctanGames.Infrastructure.Services.PersistentProgress;
using OctanGames.Logic;
using OctanGames.Services.Input;
using UnityEngine;

namespace OctanGames.Hero
{
    [RequireComponent(typeof(HeroAnimator))]
    [RequireComponent(typeof(CharacterController))]
    public class HeroAttack : MonoBehaviour, ISavedProgressReader
    {
        [SerializeField] private HeroAnimator _animator;
        [SerializeField] private CharacterController _characterController;

        private IInputService _input;

        private readonly Collider[] _hits = new Collider[3];
        private static int _layerMask;
        private Stats _stats;

        private void Awake()
        {
            _input = ServiceLocator.Container.Single<IInputService>();
            _layerMask = 1 << LayerMask.NameToLayer("Hittable");
        }

        private void Update()
        {
            if (!_input.IsAttackButtonUp() || _animator.IsAttacking) return;

            _animator.PlayAttack();
        }

        private void OnAttack()
        {
            int hitCount = Hit();
            for (var i = 0; i < hitCount; i++)
            {
                if (_hits[i].transform.parent.TryGetComponent(out IHealth health))
                {
                    health.TakeDamage(_stats.Damage);
                }
            }
        }

        private int Hit() =>
            Physics.OverlapSphereNonAlloc(StartPoint() + transform.forward, _stats.DamageRadius, _hits, _layerMask);

        void ISavedProgressReader.LoadProgress(PlayerProgress progress) => _stats = progress.PlayerStats;

        private Vector3 StartPoint() =>
            new(transform.position.x, _characterController.center.y / 2, transform.position.z);
    }
}