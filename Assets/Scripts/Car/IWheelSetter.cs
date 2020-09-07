using UnityEngine;

namespace Car
{
    public interface IWheelSetter
    {
        Transform GetWheelBackLeft();
        Transform GetWheelBackRight();
        Transform GetWheelFrontLeft();
        Transform GetWheelFrontRight();
    }
}