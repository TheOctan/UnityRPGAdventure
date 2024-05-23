using OctanGames.Infrastructure.Services;
using UnityEngine;

namespace OctanGames.Infrastructure.AssetManagement
{
    public interface IAssetProvider : IService
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 position);
    }
}