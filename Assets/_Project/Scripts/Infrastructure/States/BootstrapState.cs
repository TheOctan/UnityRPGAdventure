using System;
using OctanGames.Infrastructure.AssetManagement;
using OctanGames.Infrastructure.Factory;
using OctanGames.Infrastructure.Services;
using OctanGames.Infrastructure.Services.PersistentProgress;
using OctanGames.Infrastructure.Services.SaveLoad;
using OctanGames.Services;
using OctanGames.Services.Input;
using OctanGames.StaticData;
using UnityEngine;

namespace OctanGames.Infrastructure.States
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
            _stateMachine.Enter<LoadProgressState>();
        }

        private void RegisterServices()
        {
            _serviceLocator.RegisterSingle<IInputService>(InputService());
            _serviceLocator.RegisterSingle<IAssetProvider>(new AssetProvider());
            _serviceLocator.RegisterSingle<IPlayerProgressService>(new PlayerProgressService());
            RegisterStaticData();

            var assets = _serviceLocator.Single<IAssetProvider>();
            _serviceLocator.RegisterSingle<IGameFactory>(new GameFactory(assets,
                _serviceLocator.Single<IStaticDataService>()));

            var progressService = _serviceLocator.Single<IPlayerProgressService>();
            var gameFactory = _serviceLocator.Single<IGameFactory>();
            _serviceLocator.RegisterSingle<ISaveLoadService>(new SaveLoadService(progressService, gameFactory));
        }

        private void RegisterStaticData()
        {
            var staticDataService = new StaticDataService();
            staticDataService.LoadMonsters();
            _serviceLocator.RegisterSingle<IStaticDataService>(staticDataService);
        }

        private static IInputService InputService()
        {
            if (Application.isEditor) return new StandaloneInputService();
            if (Application.isMobilePlatform) return new MobileInputService();
            throw new NotSupportedException("Input is not supported on this platform");
        }
    }
}