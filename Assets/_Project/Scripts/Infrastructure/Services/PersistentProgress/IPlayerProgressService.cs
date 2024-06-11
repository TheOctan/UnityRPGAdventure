using OctanGames.Data;

namespace OctanGames.Infrastructure.Services.PersistentProgress
{
    public interface IPlayerProgressService : IService
    {
        PlayerProgress Progress { get; set; }
    }
}