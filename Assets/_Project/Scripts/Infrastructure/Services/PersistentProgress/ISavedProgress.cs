using OctanGames.Data;

namespace OctanGames.Infrastructure.Services.PersistentProgress
{
    public interface ISavedProgress : ISavedProgressReader
    {
        void SaveProgress(PlayerProgress progress);
    }
}