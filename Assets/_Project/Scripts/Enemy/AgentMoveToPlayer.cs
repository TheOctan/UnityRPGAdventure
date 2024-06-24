using OctanGames.Infrastructure.Factory;
using OctanGames.Infrastructure.Services;
using UnityEngine;
using UnityEngine.AI;

namespace OctanGames.Enemy
{
    public class AgentMoveToPlayer : Follow
    {
        private const float MINIMAL_DISTANCE = 1f;

        [SerializeField] private NavMeshAgent _agent;

        private IGameFactory _gameFactory;
        private Transform _heroTransform;

        private void Start()
        {
            _gameFactory = ServiceLocator.Container.Single<IGameFactory>();

            if (_gameFactory.HeroGameObject == null)
            {
                _gameFactory.HeroCreated += OnHeroCreated;
            }
            else
            {
                InitializeHeroTransform();
            }
        }

        private void Update()
        {
            if (Initialized() && HeroNotReached())
            {
                _agent.destination = _heroTransform.position;
            }
        }

        private void OnHeroCreated() =>
            InitializeHeroTransform();

        private void InitializeHeroTransform() =>
            _heroTransform = _gameFactory.HeroGameObject.transform;

        private bool Initialized() => _heroTransform != null;

        private bool HeroNotReached() =>
            Vector3.Distance(_agent.transform.position, _heroTransform.position) >= MINIMAL_DISTANCE;
    }
}