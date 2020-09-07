using UnityEngine;

namespace Car
{
    public class CarVisualsView : MonoBehaviour
    {
        private enum WheelSetter
        {
            ManualInspector,
            AutoByNamePattern
        }

        [SerializeField]
        private WheelSetter wheelSetMethod = WheelSetter.ManualInspector;

        private Transform _wheelBackLeft;
        private Transform _wheelBackRight;
        private Transform _wheelFrontLeft;
        private Transform _wheelFrontRight;

        private IWheelSetter _wheelSetter;

        public void SetWheelTransforms()
        {
            switch (wheelSetMethod)
            {
                case WheelSetter.AutoByNamePattern:
                    _wheelSetter = new NameWheelSetter(transform.GetChild(0));
                    break;
            }

            _wheelBackLeft = _wheelSetter?.GetWheelBackLeft();
            _wheelBackRight = _wheelSetter?.GetWheelBackRight();
            _wheelFrontLeft = _wheelSetter?.GetWheelFrontLeft();
            _wheelFrontRight = _wheelSetter?.GetWheelFrontRight();
        }

        public void SetWheelSpeed(float speed)
        {
            if (_wheelBackLeft == null)
            {
                Debug.LogWarning("Wheels are not set!");
                return;
            }

            SetWheelRotation(_wheelBackLeft, speed);
            SetWheelRotation(_wheelBackRight, speed);
            SetWheelRotation(_wheelFrontLeft, speed);
            SetWheelRotation(_wheelFrontRight, speed);
        }

        private void SetWheelRotation(Transform wheelTransform, float speed)
        {
            wheelTransform.Rotate(speed, 0f, 0f);
        }
    }
}