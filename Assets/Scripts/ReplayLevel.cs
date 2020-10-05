using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReplayLevel : MonoBehaviour
{
    public GameObject canvas;
    private int lapCounter=0;

    private void OnTriggerEnter(Collider other)
    {
        lapCounter++;
        if (lapCounter > 3)
        {
            Invoke("EnableCanvas", 1f);
            other.GetComponent<Car.CarMovement>().enabled = false;
        }
        
    }

    public void Reset()
    {
        lapCounter = 0;
        canvas.SetActive(false);
        SceneManager.LoadScene("Main");
    }

    public void EnableCanvas()
    {
        canvas.SetActive(true);
    }
}
