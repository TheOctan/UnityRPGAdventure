using System;
using OctanGames.Infrastructure.AssetManagement;
using OctanGames.Infrastructure.Factory;
using OctanGames.Infrastructure.Services;
using OctanGames.Infrastructure.Services.PersistentProgress;
using OctanGames.Infrastructure.States;
using OctanGames.Services.Input;
using UnityEngine;

namespace OctanGames.Infrastructure
{
    public class BootstrapState : IState
    {
        private const string INITIAL_SCENE = "Initial";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly ServiceLocator _serviceLocator;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, ServiceLocator services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _serviceLocator = services;

            RegisterServices();
        }

        void IState.Enter()
        {
            _sceneLoader.Load(INITIAL_SCENE, onLoaded: EnterLoadLevel);
        }

        void IExitableState.Exit()
        {
        }

        private void EnterLoadLevel()
        {
            //_stateMachine.Enter<LoadLevelState, string>(MAIN_SCENE);
        }

        private void RegisterServices()
        {
            _serviceLocator.RegisterSingle<IInputService>(InputService());
            _serviceLocator.RegisterSingle<IAssetProvider>(new AssetProvider());
            _serviceLocator.RegisterSingle<IPlayerProgressService>(new PlayerProgressService());
            var assets = _serviceLocator.Single<IAssetProvider>();
            _serviceLocator.RegisterSingle<IGameFactory>(new GameFactory(assets));
        }

        private static IInputService InputService()
        {
            if (Application.isEditor) return new StandaloneInputService();
            if (Application.isMobilePlatform) return new MobileInputService();
            throw new NotSupportedException("Input is not supported on this platform");
        }

    }
}