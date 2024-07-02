using OctanGames.Infrastructure.Services;

namespace OctanGames.Services
{
  public interface IRandomService : IService
  {
    int Next(int minValue, int maxValue);
  }
}