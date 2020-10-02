using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Pickup : MonoBehaviour
{
    public GameObject powerupEffectObj;
   // public GameObject pickupPowerupObj;
    public static bool added;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(PowerUpEffect(other));
        }
    }

    IEnumerator PowerUpEffect(Collider playerCollider)
    {
        //instantiate the effect!
        Instantiate(powerupEffectObj, transform.position, transform.rotation);
        //instantiate the pickup on the player gameobj!
        if (playerCollider.transform.childCount < 1)
        {
            GameObject pickupobj = PhotonNetwork.Instantiate("Bomb", playerCollider.transform.position, Quaternion.identity);
            pickupobj.transform.SetParent(playerCollider.transform);
            //
            added = true;
        }
        
        //deactivating the mesh and collider before destroying the gameobject later!
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
