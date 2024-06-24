using System;
using OctanGames.Infrastructure.Factory;
using OctanGames.Infrastructure.Services;
using UnityEngine;

namespace OctanGames.Enemy
{
    public class RotateToHero : Follow
    {
        [Header("Properties")]
        [SerializeField] private float _speed;

        private IGameFactory _gameFactory;
        private Transform _heroTransform;
        private Vector3 _positionToLook;

        private void Start()
        {
            _gameFactory = ServiceLocator.Container.Single<IGameFactory>();

            if (HeroExists())
            {
                InitializeHeroTransform();
            }
            else
            {
                _gameFactory.HeroCreated += OnHeroCreated;
            }
        }

        private void Update()
        {
            if (Initialized())
            {
                RotateTowardHero();
            }
        }

        private void RotateTowardHero()
        {
            UpdatePositionToLookAt();
            transform.rotation = SmoothedRotation(transform.rotation, _positionToLook);
        }

        private void UpdatePositionToLookAt()
        {
            Vector3 position = transform.position;
            Vector3 positionDiff = _heroTransform.position - position;
            _positionToLook = new Vector3(positionDiff.x, position.y, positionDiff.z);
        }

        private Quaternion SmoothedRotation(Quaternion rotation, Vector3 positionToLook) =>
            Quaternion.Lerp(rotation, Quaternion.LookRotation(positionToLook), _speed * Time.deltaTime);

        private bool HeroExists() =>
            _gameFactory.HeroGameObject != null;
        private void OnHeroCreated() =>
            InitializeHeroTransform();
        private void InitializeHeroTransform() =>
            _heroTransform = _gameFactory.HeroGameObject.transform;
        private bool Initialized() => _heroTransform != null;
    }
}