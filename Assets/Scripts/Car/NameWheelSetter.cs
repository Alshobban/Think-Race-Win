using UnityEngine;

namespace Car
{
    public class NameWheelSetter : IWheelSetter
    {
        private readonly Transform _parent;

        public NameWheelSetter(Transform parent)
        {
            _parent = parent;
        }

        public Transform GetWheelBackLeft()
        {
            return _parent.Find("wheel_backLeft");
        }

        public Transform GetWheelBackRight()
        {
            return _parent.Find("wheel_backRight");
        }

        public Transform GetWheelFrontLeft()
        {
            return _parent.Find("wheel_frontLeft");
        }

        public Transform GetWheelFrontRight()
        {
            return _parent.Find("wheel_frontRight");
        }
    }
}