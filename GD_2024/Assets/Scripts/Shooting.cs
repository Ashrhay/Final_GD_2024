using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab; // Reference to the bullet prefab
    public Transform bulletSpawnPoint; // Reference to the bullet spawn point
    public float bulletSpeed = 20f; // Speed of the bullet
    public GameObject flashEffectPrefab;

    void Update()
    {
        // Check for shooting input (e.g., left mouse button)
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Instantiate the bullet at the spawn point's position and rotation
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        // Get the Rigidbody component and set its velocity
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = bulletSpawnPoint.forward * bulletSpeed; // Move the bullet forward

        // Instantiate the flash effect at the spawn point
        GameObject flashEffect = Instantiate(flashEffectPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        // Optionally, destroy the flash effect after a short duration
        Destroy(flashEffect, 0.5f); // Adjust the duration as needed
    }

    
}

