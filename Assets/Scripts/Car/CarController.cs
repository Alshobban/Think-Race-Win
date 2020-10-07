using UnityEngine;

namespace Car
{
    public class CarController : MonoBehaviour
    {
        private CarMovementView _carMovementView;
        private CarVisualsView _carVisualsView;

        private void Awake()
        {
            _carMovementView = GetComponentInChildren<CarMovementView>();
        }

        private void Start()
        {
            GetComponent<CarSpawner>()?.SpawnRandomCar();
            
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