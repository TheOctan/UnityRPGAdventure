using OctanGames.CameraLogic;
using OctanGames.Infrastructure.Factory;
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

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
        }

        void IPayLoadedState<string>.Enter(string sceneName)
        {
            _curtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        void IExitableState.Exit()
        {
            _curtain.Hide();
        }

        private void OnLoaded()
        {
            GameObject initialPoint = GameObject.FindWithTag(INITIAL_POINT_TAG);
            GameObject hero = _gameFactory.CreateHero(initialPoint);
            _gameFactory.CreateHud();

            CameraFollow(hero);

            _stateMachine.Enter<GameLoopState>();
        }

        private void CameraFollow(GameObject gameObject)
        {
            Camera.main
                .GetComponent<CameraFollow>()
                .Follow(gameObject);
        }
    }
}