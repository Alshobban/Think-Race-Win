using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public GameObject powerupEffectObj;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine( PowerUpEffect(other));
        }
    }

    IEnumerator PowerUpEffect(Collider playerCollider)
    {
        //instantiate the effect!
        Instantiate(powerupEffectObj, transform.position, transform.rotation);

        //speeding up the car ?!
        Car.CarMovement carmov = playerCollider.GetComponentInChildren<Car.CarMovement>();
        float accelerationInitialValue = 90;
        float accelerationnewValue = Random.Range(20, 25);
        carmov.Acceleration += accelerationnewValue;

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(5f);

        //getting back to normal speed!
        carmov.Acceleration = accelerationInitialValue;

        Destroy(gameObject);
    }
}
