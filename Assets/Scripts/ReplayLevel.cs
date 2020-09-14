using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReplayLevel : MonoBehaviour
{
    public GameObject canvas;

    private void OnTriggerEnter(Collider other)
    {
        Invoke("EnableCanvas", 2f);
    }

    public void Reset()
    {
        canvas.SetActive(false);
        SceneManager.LoadScene("Game");
    }

    public void EnableCanvas()
    {
        canvas.SetActive(true);
    }
}
