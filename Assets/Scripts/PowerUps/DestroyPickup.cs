using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPickup : MonoBehaviour
{
    public GameObject powerupEffectObj;

    void Update()
    {
        if (Pickup.added && Input.GetKey(KeyCode.Q))
        {
            //Transform bomb = transform.GetChild(transform.childCount - 1).GetChild(0);
            transform.parent = null;
            //GetComponent<Rigidbody>().AddForce(transform.forward * 250);
            GetComponent<Rigidbody>().transform.rotation = GameObject.FindGameObjectWithTag("Cam").transform.rotation;
            GetComponent<Rigidbody>().AddRelativeForce(transform.forward * 250);
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<SphereCollider>().enabled = true;
            //
            Pickup.added = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            StartCoroutine(PickupDestroyed(other));
        }
            
    }
    IEnumerator PickupDestroyed(Collider playerCollider)
    {
        //instantiate the effect!
        Instantiate(powerupEffectObj, transform.position, transform.rotation);
        //deactivating the mesh and collider before destroying the gameobject later!
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;

        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
