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

        private Transform _heroTransform;
        private Vector3 _positionToLook;

        public void Construct(Transform heroTransform) => _heroTransform = heroTransform;

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

        private bool Initialized() => _heroTransform != null;
    }
}