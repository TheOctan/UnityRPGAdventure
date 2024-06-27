using System;
using UnityEngine;

namespace OctanGames.CameraLogic
{
    public class CameraFollow : MonoBehaviour
    {
        [Header("Properties")]
        [SerializeField] private float _rotationAngleX;
        [SerializeField] private float _distance;
        [SerializeField] private float _offsetY;

        private Transform _following;

        private void LateUpdate()
        {
            if (ReferenceEquals(_following, null)) return;

            Quaternion rotation = Quaternion.Euler(_rotationAngleX, 0, 0);
            Vector3 position = rotation * new Vector3(0, 0, -_distance) + FollowingPointPosition();

            Transform selfTransform = transform;
            selfTransform.rotation = rotation;
            selfTransform.position = position;
        }

        public void Follow(GameObject following)
        {
            _following = following.transform;
        }

        private Vector3 FollowingPointPosition()
        {
            Vector3 followingPosition = _following.position;
            followingPosition.y += _offsetY;

            return followingPosition;
        }
    }
}