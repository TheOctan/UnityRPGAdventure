using System.Collections.Generic;
using System.Linq;
using OctanGames.Data;
using OctanGames.Services;
using OctanGames.StaticData.Windows;
using OctanGames.UI.Services.Windows;
using UnityEngine;

namespace OctanGames.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string STATIC_DATA_MONSTERS = "StaticData/Monsters";
        private const string STATIC_DATA_LEVELS = "StaticData/Levels";
        private const string STATIC_DATA_WINDOWS = "StaticData/UI/WindowStaticData";

        private Dictionary<MonsterType, MonsterStaticData> _monsters;
        private Dictionary<string, LevelStaticData> _levels;
        private Dictionary<WindowType, WindowConfig> _windows;

        public void LoadMonsters()
        {
            _monsters = Resources
                .LoadAll<MonsterStaticData>(STATIC_DATA_MONSTERS)
                .ToDictionary(e => e.MonsterTypeId, e => e);
            
            _levels = Resources
                .LoadAll<LevelStaticData>(STATIC_DATA_LEVELS)
                .ToDictionary(e => e.LevelKey, e => e);

            _windows = Resources
                .Load<WindowsStaticData>(STATIC_DATA_WINDOWS)
                .Configs
                .ToDictionary(e => e.WindowType, e => e);
        }

        public WindowConfig ForWindow(WindowType windowType) => _windows.GetValueOrNull(windowType);
        public MonsterStaticData ForMonster(MonsterType type) => _monsters.GetValueOrNull(type);
        public LevelStaticData ForLevel(string sceneKey) => _levels.GetValueOrNull(sceneKey);
    }
}