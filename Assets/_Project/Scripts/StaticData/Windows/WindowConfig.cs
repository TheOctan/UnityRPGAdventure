using System;
using OctanGames.UI.Services.Windows;
using OctanGames.UI.Windows;

namespace OctanGames.StaticData.Windows
{
    [Serializable]
    public class WindowConfig
    {
        public WindowType WindowType;
        public WindowBase Prefab;
    }
}