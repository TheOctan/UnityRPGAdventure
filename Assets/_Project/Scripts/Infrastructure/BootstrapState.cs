using System;
using OctanGames.Services.Input;
using UnityEngine;

namespace OctanGames.Infrastructure
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;

        public BootstrapState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        void IState.Enter()
        {
            RegisterServices();
        }

        void IState.Exit()
        {
            
        }

        private void RegisterServices()
        {
            Game.InputService = RegisterInputService();
        }

        private static IInputService RegisterInputService()
        {
            if (Application.isEditor) return new StandaloneInputService();
            if (Application.isMobilePlatform) return new MobileInputService();
            throw new NotSupportedException("Input is not supported on this platform");
        }

    }
}