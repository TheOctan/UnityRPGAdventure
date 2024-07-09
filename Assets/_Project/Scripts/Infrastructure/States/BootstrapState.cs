using System;
using OctanGames.Infrastructure.AssetManagement;
using OctanGames.Infrastructure.Factory;
using OctanGames.Infrastructure.Services;
using OctanGames.Infrastructure.Services.PersistentProgress;
using OctanGames.Infrastructure.Services.SaveLoad;
using OctanGames.Services;
using OctanGames.Services.Input;
using OctanGames.StaticData;
using OctanGames.UI.Services.Factory;
using OctanGames.UI.Services.Windows;
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
            _serviceLocator.RegisterSingle<IRandomService>(new UnityRandomService());
            RegisterStaticData();

            var assetProvider = _serviceLocator.Single<IAssetProvider>();
            var staticDataService = _serviceLocator.Single<IStaticDataService>();
            _serviceLocator.RegisterSingle<IGameFactory>(new GameFactory(
                assetProvider, staticDataService,
                _serviceLocator.Single<IRandomService>(),
                _serviceLocator.Single<IPlayerProgressService>()));

            var progressService = _serviceLocator.Single<IPlayerProgressService>();
            var gameFactory = _serviceLocator.Single<IGameFactory>();
            _serviceLocator.RegisterSingle<ISaveLoadService>(new SaveLoadService(progressService, gameFactory));

            _serviceLocator.RegisterSingle<IUIFactory>(new UIFactory(assetProvider, staticDataService));
            _serviceLocator.RegisterSingle<IWindowService>(new WindowService(_serviceLocator.Single<IUIFactory>()));
            
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