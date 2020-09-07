using UnityEngine;

namespace Car
{
    public class FollowTransform : MonoBehaviour
    {
        [SerializeField]
        private Transform transformToFollow;

        [SerializeField]
        private Vector3 offset;

        private void Update()
        {
            transform.position = transformToFollow.position + offset;
        }
    }
}