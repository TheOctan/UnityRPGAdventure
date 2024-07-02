using UnityEngine;

namespace OctanGames.Enemy
{
    public class EndlessRotation : MonoBehaviour
    {
        [SerializeField] private float _speed = 100f;

        private void Update() =>
            transform.rotation *= Quaternion.Euler(0, _speed * Time.deltaTime, 0);
    }
}