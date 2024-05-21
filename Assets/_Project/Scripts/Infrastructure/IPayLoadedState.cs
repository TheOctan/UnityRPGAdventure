namespace OctanGames.Infrastructure
{
    public interface IPayLoadedState<in TPayload> : IExitableState
    {
        void Enter(TPayload payload);
    }
}