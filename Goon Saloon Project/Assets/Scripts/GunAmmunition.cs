using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GunAmmunition
{
    public UnityEvent OnBeginReloading = new ();
    public UnityEvent OnEndReloading = new ();


    private const int maxAmmo = 2;
    private int currentAmmo;
    private bool isReloading;

    public int GetAmmo
    {
        get { return currentAmmo; } // Only a getter, no setter
    }
    public bool IsReloading
    {
        get { return isReloading; } // Only a getter, no setter
    }
    
    public void AddBullet()
    {
        currentAmmo++;

        if (currentAmmo != maxAmmo) 
            return;
        
        isReloading = false;
        OnEndReloading.Invoke();
    }

    public void RemoveBullet()
    {
        currentAmmo--;

        if (currentAmmo > 0) 
            return;
        
        isReloading = true;
        OnBeginReloading.Invoke();
    }
    
    
    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("PlayerHand") && gunScript.bulletsInGun != maxAmmo)
    //     {
    //         gunScript.bulletsInGun = maxAmmo; 
    //         Debug.Log("Maximum ammunition!");
    //         
    //         gameObject.SetActive(false); 
    //     }
    //
    //     if (other.CompareTag("PlayerHand") && gunScript.ammo == maxAmmo)
    //     {
    //         Debug.Log("Already MAx!");
    //     }
    // }
}
