using OctanGames.CameraLogic;
using OctanGames.Infrastructure.Factory;
using OctanGames.Infrastructure.Services.PersistentProgress;
using OctanGames.Logic;
using UnityEngine;

namespace OctanGames.Infrastructure.States
{
    public class LoadLevelState : IPayLoadedState<string>
    {
        private const string INITIAL_POINT_TAG = "InitialPoint";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPlayerProgressService _progressService;

        public LoadLevelState(
            GameStateMachine stateMachine,
            SceneLoader sceneLoader,
            LoadingCurtain curtain,
            IGameFactory gameFactory,
            IPlayerProgressService progressService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
        }

        void IPayLoadedState<string>.Enter(string sceneName)
        {
            _curtain.Show();
            _gameFactory.Cleanup();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        void IExitableState.Exit()
        {
            _curtain.Hide();
        }

        private void OnLoaded()
        {
            InitialGameWorld();
            NotifyProgressReaders();

            _stateMachine.Enter<GameLoopState>();
        }

        private void InitialGameWorld()
        {
            GameObject initialPoint = GameObject.FindWithTag(INITIAL_POINT_TAG);
            GameObject hero = _gameFactory.CreateHero(initialPoint);
            _gameFactory.CreateHud();

            CameraFollow(hero);
        }

        private void NotifyProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
            {
                progressReader.LoadProgress(_progressService.Progress);
            }
        }

        private static void CameraFollow(GameObject gameObject)
        {
            Camera.main
                .GetComponent<CameraFollow>()
                .Follow(gameObject);
        }
    }
}