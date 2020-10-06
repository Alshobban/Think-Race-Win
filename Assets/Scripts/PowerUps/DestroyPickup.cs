using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DestroyPickup : MonoBehaviour
{
    //public GameObject powerupEffectObj;
    private bool added = false;
    private float distance= 10.0f;
    [SerializeField]
    private string powerupEffectObj = "Explode9";

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
                GetComponent<Rigidbody>().useGravity = true;
                GetComponent<Rigidbody>().detectCollisions = true;
                GetComponent<Rigidbody>().isKinematic = false;
                GetComponent<Rigidbody>().AddForce(transform.forward * 350f, ForceMode.Impulse);
                
                Pickup.added = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            //instantiate the effect!
            Debug.Log("entered");
            PhotonNetwork.Instantiate(powerupEffectObj, transform.position, transform.rotation);
            //deactivating the mesh and collider before destroying the gameobject later!
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<SphereCollider>().enabled = false;
            PhotonNetwork.Destroy(gameObject);


        }
            
    }
 

}
