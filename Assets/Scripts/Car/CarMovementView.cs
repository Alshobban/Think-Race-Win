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

        [Range(0f, 1f)]
        [SerializeField]
        private float hillClimbSmoothing;

        public float WheelSpeedMultiplier => wheelSpeedMultiplier;

        private void Awake()
        {
            SetValues();
        }

        private void OnValidate()
        {
            SetValues();
        }

        private void SetValues()
        {
            transform.GetComponentInChildren<CarMovement>().Acceleration = maxAcceleration;
            transform.GetComponentInChildren<SteerRotate>().RotationSpeed = steeringSpeed;
            transform.GetComponentInChildren<AlignToGroundNormal>().RotationSmoothing = hillClimbSmoothing;
        }
    }
}