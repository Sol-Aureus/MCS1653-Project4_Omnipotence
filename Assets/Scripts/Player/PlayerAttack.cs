using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform bulletPoint;
    [SerializeField] private GameObject bulletPrefab;

    [Header("Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    [SerializeField] private float damage;
    [SerializeField] private float bulletSize;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    // Shoots a bullet
    private void Shoot()
    {
        // Create a bullet instance
        GameObject bullet = Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);

        // Configure the bullet's attributes
        bullet.GetComponent<BulletMovement>().SetAttributes(speed, lifeTime, damage, bulletSize);
    }
}
