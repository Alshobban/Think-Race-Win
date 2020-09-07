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

        [SerializeField]
        private Transform wheelBackLeft;

        [SerializeField]
        private Transform wheelBackRight;

        [SerializeField]
        private Transform wheelFrontLeft;

        [SerializeField]
        private Transform wheelFrontRight;

        private IWheelSetter _wheelSetter;

        public void SetWheelTransforms()
        {
            switch (wheelSetMethod)
            {
                case WheelSetter.AutoByNamePattern:
                    _wheelSetter = new NameWheelSetter(transform.GetChild(0));
                    break;
            }

            wheelBackLeft = _wheelSetter?.GetWheelBackLeft();
            wheelBackRight = _wheelSetter?.GetWheelBackRight();
            wheelFrontLeft = _wheelSetter?.GetWheelFrontLeft();
            wheelFrontRight = _wheelSetter?.GetWheelFrontRight();
        }

        public void SetWheelSpeed(float speed)
        {
            if (wheelBackLeft == null)
            {
                Debug.LogWarning("Wheels are not set!");
                return;
            }

            SetWheelRotation(wheelBackLeft, speed);
            SetWheelRotation(wheelBackRight, speed);
            SetWheelRotation(wheelFrontLeft, speed);
            SetWheelRotation(wheelFrontRight, speed);
        }

        private void SetWheelRotation(Transform wheelTransform, float speed)
        {
            wheelTransform.Rotate(speed, 0f, 0f);
        }
    }
}