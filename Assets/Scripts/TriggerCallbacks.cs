using System;
using UnityEngine;

public class TriggerCallbacks : MonoBehaviour
{
    public event Action<Collider> TriggerEntered;
    public event Action<Collider> TriggerExit;
    public event Action<Collider> TriggerStaying;

    private void OnTriggerEnter(Collider other)
    {
        TriggerEntered?.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        TriggerExit?.Invoke(other);
    }

    private void OnTriggerStay(Collider other)
    {
        TriggerStaying?.Invoke(other);
    }
}