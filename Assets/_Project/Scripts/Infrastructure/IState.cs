namespace OctanGames.Infrastructure
{
    public interface IState
    {
        void Enter();
        void Exit();
    }
}