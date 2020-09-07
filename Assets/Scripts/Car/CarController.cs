using UnityEngine;

namespace Car
{
    public class CarController : MonoBehaviour
    {
        private CarMovementView _carMovementView;
        private CarVisualsView _carVisualsView;
        private CarSpawner _carSpawner;

        private void Awake()
        {
            _carMovementView = GetComponentInChildren<CarMovementView>();
            _carSpawner = GetComponentInChildren<CarSpawner>();
        }

        private void Start()
        {
            _carSpawner.SpawnRandomCar();

            _carVisualsView = GetComponentInChildren<CarVisualsView>();
            _carVisualsView.SetWheelTransforms();
        }

        private void UpdateWheelSpeed(float speed)
        {
            _carVisualsView.SetWheelSpeed(speed * _carMovementView.WheelSpeedMultiplier);
        }

        private void OnEnable()
        {
            GetComponentInChildren<CarMovement>().SpeedChanged += UpdateWheelSpeed;
        }

        private void OnDisable()
        {
            GetComponentInChildren<CarMovement>().SpeedChanged -= UpdateWheelSpeed;
        }
    }
}