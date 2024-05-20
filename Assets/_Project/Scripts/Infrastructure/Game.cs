using System;
using OctanGames.Services.Input;
using UnityEngine.Device;

namespace OctanGames.Infrastructure
{
    public class Game
    {
        public static IInputService InputService;

        public Game()
        {
            RegisterInputService();
        }

        private static void RegisterInputService()
        {
            if (Application.isEditor)
            {
                InputService = new StandaloneInputService();
            }
            else if (Application.isMobilePlatform)
            {
                InputService = new MobileInputService();
            }
            else
            {
                throw new NotSupportedException("Input is not supported on this platform");
            }
        }
    }
}