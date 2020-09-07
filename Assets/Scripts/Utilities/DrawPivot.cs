using System;
using UnityEngine;

namespace Utilities
{
    [ExecuteInEditMode]
    public class DrawPivot : MonoBehaviour
    {
        [SerializeField]
        private float sphereRadius = 1f;

        [SerializeField]
        private Color pivotColor = Color.green;

        private void OnDrawGizmos()
        {
            Gizmos.color = pivotColor;
            Gizmos.DrawSphere(transform.position, sphereRadius);
        }
    }
}