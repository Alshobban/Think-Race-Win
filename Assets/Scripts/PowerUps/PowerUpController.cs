using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PowerUpController : MonoBehaviour
{
    public GameObject[] powerBoost;
    public GameObject[] pickups;
    [SerializeField]
    private string powerBoostObj="BoostPowerUp";
    [SerializeField]
    private string pickupObj= "PickupPowerUp";
    // Start is called before the first frame update
    void Start()
    {
        InstantiatingPowerBoosts(powerBoostObj,powerBoost);
        InstantiatingPickups(pickupObj,pickups);
    }

    void InstantiatingPowerBoosts(string name,GameObject[] powerBoostArr)
    {
        for (int i=0;i<powerBoostArr.Length;i++)
        {
            PhotonNetwork.Instantiate(name, powerBoostArr[i].transform.position, Quaternion.identity, 0);
        }
    }
    void InstantiatingPickups(string name, GameObject[] pickupsArr)
    {
        for (int i = 0; i < pickupsArr.Length; i++)
        {
            PhotonNetwork.Instantiate(name, pickupsArr[i].transform.position, Quaternion.identity, 0);
        }
    }
}
