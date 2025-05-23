using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TrailRenderer trail;

    [Header("Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    [SerializeField] private float damage;

    private Vector3 previousPos;
    private float moveDistance = 0;

    // Update is called once per frame
    void Update()
    {
        if (TimeManager.instance.IsTimeStopped())
        {
            // If time is stopped, do not move the bullet
            return;
        }
        previousPos = transform.position;

        // Move the bullet forward
        transform.Translate(new Vector3(0, 1, 0) * speed * Time.deltaTime);
        moveDistance += speed * Time.deltaTime;

        // Need to send a raycast to check if the bullet hit something (bullet moves too fast)
        if (Physics.Linecast(previousPos, transform.position, out RaycastHit hitInfo))
        {
            // Check the tag of the object hit
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                // Apply damage to the enemy
                hitInfo.collider.GetComponentInParent<EnemyHealth>().TakeDamage(damage);

                // Destroy the bullet
                Destroy(gameObject);
            }
            else if (hitInfo.collider.CompareTag("Wall"))
            {
                // Destroy the bullet on collision with a wall
                Destroy(gameObject);
            }
        }

        // Check if the bullet is out of bounds
        if (moveDistance >= lifeTime)
        {
            // Destroy the bullet if it exceeds its lifetime
            Destroy(gameObject);
        }
    }

    // Configure the bullet's attributes
    public void SetAttributes(float speed, float lifeTime, float damage)
    {
        this.speed = speed;
        this.damage = damage;
        this.lifeTime = lifeTime;

        // Set the trail renderer's time to the bullet's lifetime
        trail.time = 10 / speed;
    }
}
