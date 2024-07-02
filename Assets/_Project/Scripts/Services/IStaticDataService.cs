using OctanGames.Infrastructure.Services;
using OctanGames.StaticData;

namespace OctanGames.Services
{
    public interface IStaticDataService : IService
    {
        void LoadMonsters();
        MonsterStaticData ForMonster(MonsterType type);
    }
}