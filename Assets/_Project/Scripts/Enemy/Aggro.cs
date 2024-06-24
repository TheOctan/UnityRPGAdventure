using UnityEngine;

namespace OctanGames.Enemy
{
    public class Aggro : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private AgentMoveToPlayer _follow;

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

        private void TriggerEnter(Collider obj) => SwitchFollowOn();
        private void TriggerExit(Collider obj) => SwitchFollowOff();
        private void SwitchFollowOn() => _follow.enabled = true;
        private void SwitchFollowOff() => _follow.enabled = false;
    }
}