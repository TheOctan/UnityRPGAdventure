using Random = UnityEngine.Random;

namespace OctanGames.Services
{
  public class UnityRandomService : IRandomService
  {
    public int Next(int min, int max) =>
      Random.Range(min, max);
  }
}