using UnityEngine;
using UnityEngine.AI;

namespace OctanGames.Enemy
{
    public class AgentMoveToPlayer : Follow
    {
        private const float MINIMAL_DISTANCE = 1f;

        [SerializeField] private NavMeshAgent _agent;

        private Transform _heroTransform;

        public void Construct(Transform heroTransform) => _heroTransform = heroTransform;

        private void Update() => SetDestinationForAgent();

        private void SetDestinationForAgent()
        {
            if (!Initialized() || HeroReached()) return;
            _agent.destination = _heroTransform.position;
        }

        private bool Initialized() => _heroTransform != null;

        private bool HeroReached() =>
            Vector3.Distance(_agent.transform.position, _heroTransform.position) <= MINIMAL_DISTANCE;
    }
}