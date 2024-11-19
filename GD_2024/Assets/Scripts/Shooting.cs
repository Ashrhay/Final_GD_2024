using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab; // Reference to the bullet prefab
    public Transform bulletSpawnPoint; // Reference to the bullet spawn point
    public float bulletSpeed = 20f; // Speed of the bullet
    public GameObject flashEffectPrefab;

    public float noOfBullets; // Number of Bullets
    public TMP_Text ammoTxt;
    public GameObject Ammo;
    public AudioSource audioSource;
    public AudioClip gunNoise;
    void Start()
    {
        noOfBullets = 20f;
        ammoTxt.text = "" + noOfBullets;
    }
    void Update()
    {
        // Check for shooting input (e.g., left mouse button)
        if (Input.GetButtonDown("Fire1") && noOfBullets>0)
        {
            Shoot();
            noOfBullets = noOfBullets - 1;
            ammoTxt.text = "" + noOfBullets;
            if (noOfBullets <= 0)
            {
                ammoTxt.text = "Out of Ammo";
            }
        }
    }

    void Shoot()
    {
        // Instantiate the bullet at the spawn point's position and rotation
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
       
        // Get the Rigidbody component and set its velocity
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = bulletSpawnPoint.forward * bulletSpeed; // Move the bullet forward
        Destroy(bullet,1f); // Destroy bullet after deployed 

        // Instantiate the flash effect at the spawn point
        GameObject flashEffect = Instantiate(flashEffectPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        if (audioSource != null && gunNoise != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Gunshot sound or AudioSource is not assigned!");
        }
        // Optionally, destroy the flash effect after a short duration
        Destroy(flashEffect, 0.5f); // Adjust the duration as needed

        
    }

    void OnControllerColliderHit(ControllerColliderHit shoot)
    {
        if (shoot.gameObject.tag == "Bullet")
        {
            Debug.Log("Ammo Up");
            noOfBullets = noOfBullets + 1f;
            Destroy(shoot.gameObject);
            ammoTxt.text = "" + noOfBullets;
        }
    }

    

    
}

