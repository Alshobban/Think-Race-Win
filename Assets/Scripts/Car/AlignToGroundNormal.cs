using UnityEngine;

namespace Car
{
    public class AlignToGroundNormal : MonoBehaviour
    {
        [SerializeField]
        private LayerMask groundLayer;

        [SerializeField]
        private float maxRaycastDistance = 10f;

        [Range(0f, 1f)]
        [SerializeField]
        private float rotationSmoothing;

        public float RotationSmoothing
        {
            get => rotationSmoothing;
            set
            {
                if (value > 0f || value <= 1f)
                {
                    rotationSmoothing = value;
                }
                else
                {
                    Debug.LogError("Rotation smoothing value should be between 0f and 1f!");
                }
            }
        }

        private void Update()
        {
            if (Physics.Raycast(transform.position, Vector3.down, out var hit, maxRaycastDistance, groundLayer))
            {
                transform.up = Vector3.Lerp(transform.up, hit.normal, RotationSmoothing);
            }
        }
    }
}