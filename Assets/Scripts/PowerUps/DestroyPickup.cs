using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DestroyPickup : MonoBehaviour
{
    private bool added,entered;

    private void Start()
    {
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().detectCollisions = false;
        added = true;
    }

    void Update()
    {
        if (added)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            if (Input.GetKeyDown(KeyCode.Q))
            {
                //transform.SetParent(null);
                GetComponent<Rigidbody>().isKinematic = false;
               // GetComponent<Rigidbody>().AddRelativeForce(transform.forward * 1000f * Time.deltaTime, ForceMode.Impulse);
                GetComponent<Rigidbody>().useGravity = true;
                GetComponent<Rigidbody>().detectCollisions = true;
                transform.SetParent(null);

                Pickup.added = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            Debug.Log("entered");
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().useGravity = false;
            entered = true;
        }
        if (other.tag == "Player" && entered)
        {
            //instantiate the effect!
            Debug.Log("entered");
            PhotonNetwork.Instantiate("Explode9", transform.position, transform.rotation);
            //deactivating the mesh and collider before destroying the gameobject later!
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<SphereCollider>().enabled = false;
            PhotonNetwork.Destroy(gameObject);


        }
            
    }
 

}
