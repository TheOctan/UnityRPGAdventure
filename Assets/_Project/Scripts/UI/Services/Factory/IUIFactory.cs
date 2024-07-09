using OctanGames.Infrastructure.Services;

namespace OctanGames.UI.Services.Factory
{
    public interface IUIFactory : IService
    {
        void CreateShop();
        void CreateUIRoot();
    }
}