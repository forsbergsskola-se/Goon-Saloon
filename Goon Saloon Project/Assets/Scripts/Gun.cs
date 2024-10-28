using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;
    public int maxShots = 2;
    public int ammo = 6;
    public Transform reticle;
    
    private int currentShots = 0;

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) && currentShots < maxShots)
        {
            Shoot();
        }
        
        if (Input.GetKeyDown(KeyCode.R) && ammo > 0)
        {
            Reload();
        }
    }

    private void Shoot()
    {
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        
        Vector3 shootDirection = (reticle.position - bulletSpawnPoint.position).normalized;
        
        bullet.GetComponent<Rigidbody>().velocity = shootDirection * bulletSpeed;

        currentShots++;

        if (ammo <= 0)
        {
            Debug.Log("No ammunition.");
        }
    }

    private void Reload()
    {
        if (ammo > 0)
        {
            Debug.Log("Reloading the weapon...");
            currentShots = 0;
            ammo -= 2;
        }
        else
        {
            Debug.Log("No ammunition.");
        }
        
    }

}

