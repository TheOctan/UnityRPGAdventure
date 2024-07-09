using OctanGames.Infrastructure.Services;

namespace OctanGames.UI.Services.Windows
{
    public interface IWindowService : IService
    {
        void Open(WindowType windowType);
    }
}