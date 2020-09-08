using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwiitchScene : MonoBehaviour
{
    public GameObject canvas;
    private void Start()
    {
        canvas.gameObject.GetComponent<Canvas>().enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("plane"))
        {
            canvas.gameObject.GetComponent<Canvas>().enabled = true;
        }
            
        
        
    }
}
