using OctanGames.Data;

namespace OctanGames.Infrastructure.Services.PersistentProgress
{
    public interface ISavedProgressWriter : ISavedProgressReader
    {
        void SaveProgress(PlayerProgress progress);
    }
}