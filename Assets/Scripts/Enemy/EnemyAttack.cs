using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform bulletPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioClip shootSound;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float lifeTime;
    [SerializeField] private float damage;
    [SerializeField] private float burstCooldown;
    [SerializeField] private int burstTimes;
    [SerializeField] private float fireRate;

    private bool isAttacking;
    private bool isBursting;
    private float burstCooldownTimer;

    // Start is called before the first frame update
    void Start()
    {
        isAttacking = true;
        burstCooldownTimer = burstCooldown * 2; // Reset the cooldown if already bursting
    }

    // Shoots a bullet
    private void Shoot()
    {
        // Create a bullet instance
        GameObject bullet = Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);

        // Play the shoot sound
        SoundFXManager.instance.PlaySound(shootSound, transform, 1);

        // Configure the bullet's attributes
        bullet.GetComponent<EnemyBullet>().SetAttributes(bulletSpeed, lifeTime, damage);
    }

    // Coroutine for burst fire
    private IEnumerator BurstFire()
    {
        for (int i = 0; i < burstTimes; i++)
        {
            // Set the animator to attack
            animator.SetTrigger("Attack");
            Shoot();

            // Wait for the fire rate before shooting again
            yield return new WaitForSeconds(fireRate);
        }
        animator.SetTrigger("Idle");
    }

    public void Attack(bool attacking)
    {
        if (isBursting)
        {
            burstCooldownTimer = burstCooldown * 2; // Reset the cooldown if already bursting
            isBursting = false; // Reset the bursting state
        }

        if (TimeManager.instance.IsTimeStopped())
        {
            return; // Skip the update if time is stopped
        }

        // Check if the burst cooldown is active
        burstCooldownTimer -= Time.deltaTime;

        // Check if the player is trying to attack
        if (attacking)
        {
            if (!isAttacking)
            {
                isAttacking = true;
                burstCooldownTimer = burstCooldown;
                StartCoroutine(BurstFire());
            }
        }
        else
        {
            isBursting = true; // Set the bursting state to true
        }

        // Reset the attack state if the cooldown is active
        if (burstCooldownTimer <= 0)
        {
            isAttacking = false;
        }
    }
}
