using OctanGames.Data;
using OctanGames.Infrastructure.Services;
using OctanGames.Infrastructure.Services.PersistentProgress;
using OctanGames.Services.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OctanGames.Hero
{
    public class HeroMove : MonoBehaviour, ISavedProgressWriter
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _movementSpeed;

        private IInputService _inputService;
        private Camera _camera;

        private void Awake()
        {
            _inputService = ServiceLocator.Container.Single<IInputService>();
        }

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            Vector3 movementVector = Vector3.zero;

            if (_inputService.Axis.sqrMagnitude > Constants.EPSILON)
            {
                movementVector = _camera.transform.TransformDirection(_inputService.Axis);
                movementVector.y = 0;
                movementVector.Normalize();

                transform.forward = movementVector;
            }

            movementVector += Physics.gravity;

            _characterController.Move(_movementSpeed * Time.deltaTime * movementVector);
        }

        void ISavedProgressWriter.SaveProgress(PlayerProgress progress)
        {
            progress.WorldData.PositionOnLevel = new PositionOnLevel(CurrentLevel(), transform.position.AsVectorData());
        }

        void ISavedProgressReader.LoadProgress(PlayerProgress progress)
        {
            if (CurrentLevel() != progress.WorldData.PositionOnLevel.Level) return;

            Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;
            if (savedPosition == null) return;

            Warp(savedPosition);
        }

        private void Warp(Vector3Data savedPosition)
        {
            _characterController.enabled = false;
            transform.position = savedPosition.AsUnityVector();
            _characterController.enabled = true;
        }

        private static string CurrentLevel() => SceneManager.GetActiveScene().name;
    }
}