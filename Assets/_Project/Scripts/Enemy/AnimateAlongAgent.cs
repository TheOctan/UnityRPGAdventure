using System;
using OctanGames.Enemy;
using UnityEngine;
using UnityEngine.AI;

namespace OctanGames.Hero
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(EnemyAnimator))]
    public class AnimateAlongAgent : MonoBehaviour
    {
        private const float MINIMAL_SPEED = 0.1f;

        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private EnemyAnimator _animator;

        

        private void Update()
        {
            if (ShouldMove())
            {
                _animator.Move(_agent.velocity.magnitude);
            }
            else
            {
                _animator.StopMoving();
            }
        }

        private bool ShouldMove() =>
            _agent.velocity.magnitude > MINIMAL_SPEED
            && _agent.remainingDistance > _agent.radius;
    }
}