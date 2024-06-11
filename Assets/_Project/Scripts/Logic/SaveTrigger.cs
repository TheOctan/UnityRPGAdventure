using OctanGames.Infrastructure.Services;
using OctanGames.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace OctanGames.Logic
{
    public class SaveTrigger : MonoBehaviour
    {
        [SerializeField] private BoxCollider _collider;

        private ISaveLoadService _saveLoadService;

        private void Awake()
        {
            _saveLoadService = ServiceLocator.Container.Single<ISaveLoadService>();
        }

        private void OnTriggerEnter(Collider other)
        {
            _saveLoadService.SaveProgress();
            Debug.Log("Progress saved");

            gameObject.SetActive(false);
        }

        private void OnDrawGizmos()
        {
            if (_collider == null) return;

            Gizmos.color = new Color32(30, 200, 30, 130);
            Gizmos.DrawCube(transform.position + _collider.center, _collider.size);
        }
    }
}