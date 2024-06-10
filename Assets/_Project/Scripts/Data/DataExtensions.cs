using UnityEngine;

namespace OctanGames.Data
{
    public static class DataExtensions
    {
        public static Vector3Data AsVectorData(this Vector3 vector) => new(vector.x, vector.y, vector.z);
    }
}