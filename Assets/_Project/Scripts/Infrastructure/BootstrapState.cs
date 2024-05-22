using System;
using OctanGames.Infrastructure.States;
using OctanGames.Services.Input;
using UnityEngine;

namespace OctanGames.Infrastructure
{
    public class BootstrapState : IState
    {
        private const string INITIAL_SCENE = "Initial";
        private const string MAIN_SCENE = "Main";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        void IState.Enter()
        {
            RegisterServices();
            _sceneLoader.Load(INITIAL_SCENE, onLoaded: EnterLoadLevel);
        }

        void IExitableState.Exit()
        {
            
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadLevelState, string>(MAIN_SCENE);
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