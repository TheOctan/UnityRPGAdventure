using OctanGames.Data;
using OctanGames.Infrastructure.Services.PersistentProgress;
using OctanGames.Infrastructure.Services.SaveLoad;

namespace OctanGames.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private const string MAIN_SCENE = "Main";

        private readonly GameStateMachine _stateMachine;
        private readonly IPlayerProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(GameStateMachine stateMachine,
            IPlayerProgressService progressService,
            ISaveLoadService saveLoadService)
        {
            _stateMachine = stateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        void IState.Enter()
        {
            LoadProgressOrInitNew();
            _stateMachine.Enter<LoadLevelState, string>(_progressService.Progress.WorldData.PositionOnLevel.Level);
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
            var progress = new PlayerProgress(MAIN_SCENE)
            {
                PlayerStats =
                {
                    Damage = 1,
                    DamageRadius = 0.5f
                },
                PlayerState =
                {
                    MaxHP = 50
                }
            };
            progress.PlayerState.ResetHP();
            
            return progress;
        }
    }
}