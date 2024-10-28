using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammunition : MonoBehaviour
{
    public int maxAmmo = 6; 
    public Gun gunScript; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHand") && gunScript.ammo != maxAmmo)
        {
            gunScript.ammo = maxAmmo; 
            Debug.Log("Maximum ammunition!");
            
            gameObject.SetActive(false); 
        }

        if (other.CompareTag("PlayerHand") && gunScript.ammo == maxAmmo)
        {
            Debug.Log("Already MAx!");
        }
    }
}
