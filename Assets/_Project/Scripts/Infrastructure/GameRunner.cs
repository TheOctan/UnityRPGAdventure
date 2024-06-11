using System;
using UnityEngine;

namespace OctanGames.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private GameBootstrapper _bootstrapper;

        private void Awake()
        {
            var gameBootstrapper = FindObjectOfType<GameBootstrapper>();
            if (gameBootstrapper != null) return;

            Instantiate(_bootstrapper);
        }
    }
}