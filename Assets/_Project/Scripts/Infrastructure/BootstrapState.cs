using System;
using OctanGames.Infrastructure.AssetManagement;
using OctanGames.Infrastructure.Factory;
using OctanGames.Infrastructure.Services;
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
            ServiceLocator.Container.RegisterSingle<IInputService>(InputService());
            var assets = ServiceLocator.Container.Single<IAssetProvider>();
            ServiceLocator.Container.RegisterSingle<IGameFactory>(new GameFactory(assets));
        }

        private static IInputService InputService()
        {
            if (Application.isEditor) return new StandaloneInputService();
            if (Application.isMobilePlatform) return new MobileInputService();
            throw new NotSupportedException("Input is not supported on this platform");
        }

    }
}