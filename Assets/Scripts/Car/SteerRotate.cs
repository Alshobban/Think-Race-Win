using UnityEngine;

namespace Car
{
    public class SteerRotate : MonoBehaviour
    {
        [SerializeField]
        private float rotationSpeed;

        private float _horizontalInput;

        public float RotationSpeed
        {
            get => rotationSpeed;
            set => rotationSpeed = value;
        }

        private void Update()
        {
            _horizontalInput = Input.GetAxis("Horizontal");
        }

        private void FixedUpdate()
        {
            var rotateAmount = _horizontalInput * RotationSpeed;

            transform.Rotate(0f, rotateAmount, 0f);
        }
    }
}