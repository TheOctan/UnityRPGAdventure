using System;
using OctanGames.Data;
using OctanGames.Logic;
using OctanGames.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace OctanGames.Hero
{
    [RequireComponent(typeof(HeroAnimator))]
    public class HeroHealth : MonoBehaviour, ISavedProgressWriter, IHealth
    {
        public event Action HealthChanged; 

        [SerializeField] private HeroAnimator _animator;

        private PlayerState _state;

        public float Current
        {
            get => _state.CurrentHP;
            set
            {
                if (_state.CurrentHP == value) return;
                _state.CurrentHP = value;
                HealthChanged?.Invoke();
            }
        }

        public float Max
        {
            get => _state.MaxHP;
            set => _state.MaxHP = value;
        }

        public void TakeDamage(float damage)
        {
            if (Current <= 0) return;

            Current -= damage;
            _animator.PlayHit();
        }
        void ISavedProgressReader.LoadProgress(PlayerProgress progress)
        {
            _state = progress.PlayerState;
            HealthChanged?.Invoke();
        }
        void ISavedProgressWriter.SaveProgress(PlayerProgress progress)
        {
            progress.PlayerState.CurrentHP = Current;
            progress.PlayerState.MaxHP = Max;
        }
    }
}