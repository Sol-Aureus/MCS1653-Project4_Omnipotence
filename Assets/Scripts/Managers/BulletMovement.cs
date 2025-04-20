using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float damage;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Move the bullet forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Need to send a raycast to check if the bullet hit something (bullet moves too fast)
    }

    // Configure the bullet's attributes
    public void SetAttributes(float speed, float lifeTime, float damage, float bulletSize)
    {
        this.speed = speed;
        this.damage = damage;

        // Set the size of the bullet
        transform.localScale = new Vector3(bulletSize, bulletSize, bulletSize);

        // Destroy the bullet after its lifetime
        Destroy(gameObject, lifeTime);
    }

    // Handle collision with other objects
    private void OnTriggerEnter(Collider other)
    {
        // Check if the bullet hit an enemy
        if (other.CompareTag("Enemy"))
        {
            // Apply damage to the enemy
            //other.GetComponent<Enemy>().TakeDamage(damage);
            // Destroy the bullet
            Destroy(gameObject);
        }
        else if (other.CompareTag("Wall"))
        {
            // Destroy the bullet on collision with a wall
            Destroy(gameObject);
        }
    }
}
