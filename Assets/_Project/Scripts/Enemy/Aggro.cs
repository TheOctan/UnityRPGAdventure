using System.Collections;
using UnityEngine;

namespace OctanGames.Enemy
{
    public class Aggro : MonoBehaviour
    {
        [Header("Properties")]
        [SerializeField] private float _coolDown;

        [Header("Components")]
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private AgentMoveToPlayer _follow;

        private Coroutine _agroCoroutine;
        private bool _hasAgroTarget;

        private void Start()
        {
            SwitchFollowOff();

            _triggerObserver.TriggerEnter += TriggerEnter;
            _triggerObserver.TriggerExit += TriggerExit;
        }

        private void OnDestroy()
        {
            _triggerObserver.TriggerEnter -= TriggerEnter;
            _triggerObserver.TriggerExit -= TriggerExit;
        }

        private void TriggerEnter(Collider obj)
        {
            StopAgroCoroutine();
            SwitchFollowOn();
        }

        private void TriggerExit(Collider obj)
        {
            if (_hasAgroTarget) return;
            _hasAgroTarget = true;

            _agroCoroutine = StartCoroutine(SwitchFollowOffAfterCoolDown());
        }

        private void StopAgroCoroutine()
        {
            if (!_hasAgroTarget) return;
            _hasAgroTarget = false;

            if (_agroCoroutine == null) return;

            StopCoroutine(_agroCoroutine);
            _agroCoroutine = null;
        }

        private IEnumerator SwitchFollowOffAfterCoolDown()
        {
            yield return new WaitForSeconds(_coolDown);
            SwitchFollowOff();
        }

        private void SwitchFollowOn() => _follow.enabled = true;
        private void SwitchFollowOff() => _follow.enabled = false;
    }
}