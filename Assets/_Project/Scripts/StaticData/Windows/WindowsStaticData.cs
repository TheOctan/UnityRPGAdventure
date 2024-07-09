using System.Collections.Generic;
using UnityEngine;

namespace OctanGames.StaticData.Windows
{
    [CreateAssetMenu(fileName = "WindowStaticData", menuName = "Static Data/Window", order = 0)]
    public class WindowsStaticData : ScriptableObject
    {
        public List<WindowConfig> Configs = new();
    }
}