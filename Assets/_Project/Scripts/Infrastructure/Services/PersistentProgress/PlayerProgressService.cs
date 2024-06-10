using OctanGames.Data;

namespace OctanGames.Infrastructure.Services.PersistentProgress
{
    public class PlayerProgressService : IPlayerProgressService
    {
        public PlayerProgress Progress { get; set; }
    }
}