using OctanGames.CameraLogic;
using UnityEngine;

namespace OctanGames.Infrastructure
{
    public class LoadLevelState : IPayLoadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        void IPayLoadedState<string>.Enter(string sceneName)
        {
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        void IExitableState.Exit()
        {
            
        }

        private void OnLoaded()
        {
            GameObject hero = Instantiate("Hero/Hero");
            Instantiate("Hud/Hud");
            
            CameraFollow(hero);
        }

        private void CameraFollow(GameObject gameObject)
        {
            Camera.main
                .GetComponent<CameraFollow>()
                .Follow(gameObject);
        }

        private static GameObject Instantiate(string path)
        {
            var heroPrefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(heroPrefab);
        }
    }
}