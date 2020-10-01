using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowPickup : MonoBehaviour
{

    // Update is called once per frame
   /* void Update()
    {
        if(Pickup.added && Input.GetKey(KeyCode.Q))
        {
            Transform bomb = transform.GetChild(transform.childCount-1).GetChild(0);
            bomb.parent = null;
            bomb.GetComponent<Rigidbody>().AddForce(transform.forward * 250);
            //bomb.GetComponent<Rigidbody>().isKinematic = false;
           // bomb.GetComponent<SphereCollider>().enabled = true;

            Pickup.added = false;
        }
    }*/
}
