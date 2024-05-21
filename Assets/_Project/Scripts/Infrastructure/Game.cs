using OctanGames.Services.Input;

namespace OctanGames.Infrastructure
{
    public class Game
    {
        public static IInputService InputService;

        public readonly GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner));
        }
    }
}