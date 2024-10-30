using System.Linq;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;
    public Transform reticle; //Is not set inside of prefab
    public Transform barrelTransform;
    
    public Transform[] bulletTransforms;

    public AudioSource addAmmoAudio;
    public AudioSource shootAudio;
    
    private GunAmmunition gunAmmo;
    private bool isReloading;

    private void Start()
    {
        gunAmmo = new GunAmmunition();
        gunAmmo.OnBeginReloading.AddListener(OnBeginReloading);
        gunAmmo.OnEndReloading.AddListener(OnEndReloading);

    }

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) && !isReloading)
        {
            Shoot();
        }
        
        //No button should be pressed for reloading
        
        // if (Input.GetKeyDown(KeyCode.R) && ammo > 0)
        // {
        //     Reload();
        // }
    }

    private void Shoot()
    {
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        
        Vector3 shootDirection = (reticle.position - bulletSpawnPoint.position).normalized;
        
        bullet.GetComponent<Rigidbody>().velocity = shootDirection * bulletSpeed;
        shootAudio.Play();
        RemoveBullet();
        
        if (isReloading)
        {
            Debug.Log("No ammunition.");
        }
    }
    
    // private void Reload()
    // {
    //     if (ammo > 0)
    //     {
    //         Debug.Log("Reloading the weapon...");
    //         currentShots = 0;
    //         ammo -= 2;
    //     }
    //     else
    //     {
    //         Debug.Log("No ammunition.");
    //     }
    //     
    // }

    private void AddBullet()
    {
        UpdateBulletsInGun(true);
        gunAmmo.AddBullet();
        addAmmoAudio.Play();
    }

    private void RemoveBullet()
    {
        UpdateBulletsInGun(false);
        gunAmmo.RemoveBullet();
    }
    
    
    private void OnBeginReloading()
    {
        isReloading = true;
        barrelTransform.rotation = Quaternion.Euler(45, 0, 0);
    }
    private void OnEndReloading()
    {
        isReloading = false;
        barrelTransform.rotation = Quaternion.Euler(0, 0, 0);
    }

    //Visually Removes or Adds a bullet to the gun
    private void UpdateBulletsInGun(bool isAddingBullet)
    {
        //Get the first active/Inactive bullet
        var bulletTransform = bulletTransforms.FirstOrDefault(bullet => bullet.gameObject.activeSelf != isAddingBullet);

        //Set that bullet to Active/Inactive
        if (bulletTransform != null)
            bulletTransform.gameObject.SetActive(isAddingBullet);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (isReloading && other.CompareTag("Bullet"))
        {
            AddBullet();
            Destroy(other.gameObject);
        }
    }
}

