using OctanGames.Data;
using OctanGames.Infrastructure.Services.PersistentProgress;
using OctanGames.Infrastructure.Services.SaveLoad;

namespace OctanGames.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private const string MAIN_SCENE = "Main";

        private readonly GameStateMachine _gameStateMachine;
        private readonly IPlayerProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(GameStateMachine gameStateMachine,
            IPlayerProgressService progressService,
            ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        void IState.Enter()
        {
            LoadProgressOrInitNew();
            _gameStateMachine.Enter<LoadLevelState, string>(_progressService.Progress.WorldData.PositionOnLevel.Level);
        }

        void IExitableState.Exit()
        {
        }

        private void LoadProgressOrInitNew()
        {
            _progressService.Progress =
                _saveLoadService.LoadProgress()
                ?? NewProgress();
        }

        private static PlayerProgress NewProgress()
        {
            var progress = new PlayerProgress(MAIN_SCENE);
            progress.PlayerState.MaxHP = 50;
            progress.PlayerState.ResetHP();
            
            return progress;
        }
    }
}