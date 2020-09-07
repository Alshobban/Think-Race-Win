using System;
using UnityEngine;

namespace Car
{
    public class CarMovement : MonoBehaviour
    {
        public event Action<float> SpeedChanged;

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

            SpeedChanged?.Invoke(_rigidbody.velocity.sqrMagnitude);
        }

        private void OnGUI()
        {
            GUI.TextArea(new Rect(10, 10, 100, 100), _rigidbody.velocity.sqrMagnitude.ToString());
        }
    }
}