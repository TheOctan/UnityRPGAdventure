namespace OctanGames.Infrastructure
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}