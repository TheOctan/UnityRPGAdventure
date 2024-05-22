using OctanGames.Infrastructure.States;
using OctanGames.Logic;
using OctanGames.Services.Input;

namespace OctanGames.Infrastructure
{
    public class Game
    {
        public static IInputService InputService;

        public readonly GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain);
        }
    }
}