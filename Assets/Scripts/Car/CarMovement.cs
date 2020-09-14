using System;
using UnityEngine;

namespace Car
{
    public class CarMovement : MonoBehaviour
    {
        public event Action<float> SpeedChanged;

        [SerializeField]
        private float downForce = 100f;

        [SerializeField]
        private Transform visualsTransform;

        [SerializeField]
        private float acceleration;

        private Rigidbody _rigidbody;
        private float _verticalInput;

        public float Acceleration
        {
            get => acceleration;
            set => acceleration = value;
        }

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            _verticalInput = Input.GetAxis("Vertical");
        }

        private void FixedUpdate()
        {
            if (_verticalInput > 0f)
            {
                _rigidbody.AddForce(_verticalInput * visualsTransform.forward * Acceleration, ForceMode.Acceleration);
            }

            _rigidbody.AddForce(Vector3.down * downForce);

            SpeedChanged?.Invoke(_rigidbody.velocity.sqrMagnitude);
        }
    }
}