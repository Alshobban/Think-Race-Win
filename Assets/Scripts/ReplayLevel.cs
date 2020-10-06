using UnityEngine;
using UnityEngine.SceneManagement;

public class ReplayLevel : MonoBehaviour
{
    public GameObject canvas;
    private int _lapCounter;

    private void OnTriggerEnter(Collider other)
    {
        _lapCounter++;
        if (_lapCounter > 3)
        {
            Invoke(nameof(EnableCanvas), 1f);
            other.GetComponent<Car.CarMovement>().enabled = false;
        }
    }

    public void Reset()
    {
        _lapCounter = 0;
        canvas.SetActive(false);
        SceneManager.LoadScene("Main");
    }

    public void EnableCanvas()
    {
        canvas.SetActive(true);
    }
}