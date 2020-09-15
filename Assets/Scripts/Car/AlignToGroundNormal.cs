using System;
using UnityEngine;

namespace Car
{
    public class AlignToGroundNormal : MonoBehaviour
    {
        [SerializeField]
        private LayerMask groundLayer;

        [SerializeField]
        private float maxRaycastDistance = 10f;

        [SerializeField]
        private float maxRotationStep = 0.1f;

        [SerializeField]
        private float rotationSmoothing;

        public float RotationSmoothing
        {
            get => rotationSmoothing;
            set => rotationSmoothing = value;
        }

        private void FixedUpdate()
        {
            if (Physics.Raycast(transform.position, -transform.up, out var hit, maxRaycastDistance, groundLayer) &&
                Vector3.Angle(transform.up, hit.normal) > 1f)
            {
                transform.up = Vector3.Lerp(transform.up, hit.normal,
                    1f / (rotationSmoothing * Math.Max(maxRotationStep, hit.distance)));
            }
        }
    }
}