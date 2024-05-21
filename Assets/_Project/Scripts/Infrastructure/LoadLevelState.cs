using OctanGames.CameraLogic;
using UnityEngine;

namespace OctanGames.Infrastructure
{
    public class LoadLevelState : IPayLoadedState<string>
    {
        private const string INITIAL_POINT_TAG = "InitialPoint";

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
            GameObject initialPoint = GameObject.FindWithTag(INITIAL_POINT_TAG);

            GameObject hero = Instantiate("Hero/Hero", position: initialPoint.transform.position);
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
        private static GameObject Instantiate(string path, Vector3 position)
        {
            var heroPrefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(heroPrefab, position, Quaternion.identity);
        }
    }
}