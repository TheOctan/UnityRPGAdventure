using System.Collections.Generic;
using OctanGames.CameraLogic;
using OctanGames.Data;
using OctanGames.Enemy;
using OctanGames.Infrastructure.Factory;
using OctanGames.Infrastructure.Services.PersistentProgress;
using OctanGames.Logic;
using OctanGames.Services;
using OctanGames.StaticData;
using OctanGames.UI.Elements;
using OctanGames.UI.Services.Factory;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OctanGames.Infrastructure.States
{
    public class LoadLevelState : IPayLoadedState<string>
    {
        private const string INITIAL_POINT_TAG = "InitialPoint";
        private const string ENEMY_SPAWNER = "EnemySpawner";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPlayerProgressService _progressService;
        private readonly IStaticDataService _staticData;
        private readonly IUIFactory _uiFactory;

        public LoadLevelState(
            GameStateMachine stateMachine,
            SceneLoader sceneLoader,
            LoadingCurtain curtain,
            IGameFactory gameFactory,
            IPlayerProgressService progressService,
            IStaticDataService staticData,
            IUIFactory uiFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
            _staticData = staticData;
            _uiFactory = uiFactory;
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
            InitUIRoot();
            InitialGameWorld();
            NotifyProgressReaders();

            _stateMachine.Enter<GameLoopState>();
        }

        private void InitUIRoot() => _uiFactory.CreateUIRoot();

        private void InitialGameWorld()
        {
            InitSpawners();
            InitLootPieces();
            GameObject hero = InitHero();

            InitHud(hero);
            CameraFollow(hero);
        }

        private void InitSpawners()
        {
            string sceneKey = SceneManager.GetActiveScene().name;
            LevelStaticData levelData = _staticData.ForLevel(sceneKey);

            foreach (EnemySpawnerData spawnerData in levelData.EnemySpawners)
            {
                _gameFactory.CreateSpawner(spawnerData.Position, spawnerData.Id, spawnerData.MonsterType);
            }
        }

        private void InitLootPieces()
        {
            Dictionary<string, LootPieceData> lootPieceData =
                _progressService.Progress.WorldData.LootData.LootPiecesOnScene.Dictionary;

            foreach (KeyValuePair<string, LootPieceData> item in lootPieceData)
            {
                LootPiece lootPiece = _gameFactory.CreateLoot();
                lootPiece.GetComponent<UniqueId>().Id = item.Key;
                lootPiece.Initialize(item.Value.Loot);
                lootPiece.transform.position = item.Value.Position.AsUnityVector();
            }
        }

        private void NotifyProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
            {
                progressReader.LoadProgress(_progressService.Progress);
            }
        }

        private GameObject InitHero()
        {
            GameObject initialPoint = GameObject.FindWithTag(INITIAL_POINT_TAG);
            return _gameFactory.CreateHero(initialPoint);
        }

        private void InitHud(GameObject hero)
        {
            GameObject hud = _gameFactory.CreateHud();
            var heroHealth = hero.GetComponentInChildren<IHealth>();
            hud.GetComponentInChildren<ActorUI>()
                .Construct(heroHealth);
        }

        private static void CameraFollow(GameObject gameObject) =>
            Camera.main
                .GetComponent<CameraFollow>()
                .Follow(gameObject);
    }
}