using System;
using UnityEngine;

namespace Car
{
    public class CarMovementView : MonoBehaviour
    {
        [SerializeField]
        private float maxAcceleration;

        [SerializeField]
        private float steeringSpeed;

        [SerializeField]
        private float wheelSpeedMultiplier = 1f;

        [SerializeField]
        private float hillClimbSmoothing;

        [SerializeField]
        private CarMovement carMovement;

        [SerializeField]
        private SteerRotate carSteering;


        public float WheelSpeedMultiplier => wheelSpeedMultiplier;

        private void Awake()
        {
            SetValues();
        }

        private void OnValidate()
        {
            SetValues();
        }

        public void SetControls(bool isEnabled)
        {
            carMovement.enabled = isEnabled;
            carSteering.enabled = isEnabled;
        }

        private void SetValues()
        {
            transform.GetComponentInChildren<CarMovement>().Acceleration = maxAcceleration;
            transform.GetComponentInChildren<SteerRotate>().RotationSpeed = steeringSpeed;
            transform.GetComponentInChildren<AlignToGroundNormal>().RotationSmoothing = hillClimbSmoothing;
        }
    }
}