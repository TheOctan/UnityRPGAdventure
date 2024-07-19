using OctanGames.Hero;
using OctanGames.Infrastructure.Services;
using OctanGames.Infrastructure.States;
using UnityEngine;

namespace OctanGames.Logic
{
    public class LevelTransferTrigger : MonoBehaviour
    {
        [SerializeField] private string _transferTo;

        private IGameStateMachine _stateMachine;
        private bool _triggered;

        private void Awake() => _stateMachine = ServiceLocator.Container.Single<IGameStateMachine>();

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out HeroHealth _) || _triggered) return;

            _stateMachine.Enter<LoadLevelState, string>(_transferTo);
            _triggered = true;
        }
    }
}