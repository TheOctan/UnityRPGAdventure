namespace OctanGames.Infrastructure.States
{
    public interface IPayLoadedState<in TPayload> : IExitableState
    {
        void Enter(TPayload payload);
    }
}