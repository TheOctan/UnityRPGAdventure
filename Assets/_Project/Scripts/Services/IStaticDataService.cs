using OctanGames.Infrastructure.Services;
using OctanGames.StaticData;
using OctanGames.StaticData.Windows;
using OctanGames.UI.Services.Windows;

namespace OctanGames.Services
{
    public interface IStaticDataService : IService
    {
        void LoadMonsters();
        MonsterStaticData ForMonster(MonsterType type);
        LevelStaticData ForLevel(string sceneKey);
        WindowConfig ForWindow(WindowType windowType);
    }
}