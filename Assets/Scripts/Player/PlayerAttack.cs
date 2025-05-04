using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform bulletPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject muzzleFlash;
    [SerializeField] private AudioClip shootSound;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float lifeTime;
    [SerializeField] private float damage;
    [SerializeField] private float burstCooldown;
    [SerializeField] private int burstTimes;
    [SerializeField] private float fireRate;

    private bool isAttacking;
    private float burstCooldownTimer;

    private float currentFireRate;
    private float currentBurstCooldown;

    // Update is called once per frame
    void Update()
    {
        if (MenuManager.menus.isPaused)
        {
            return;
        }
        // Adjust the cooldown and fire rate based on the player's time scale
        currentBurstCooldown = burstCooldown;
        currentFireRate = fireRate;

        // Check if the burst cooldown is active
        burstCooldownTimer -= Time.deltaTime * TimeManager.instance.GetPlayerTimeScale();

        // Check if the player is trying to attack
        if (Input.GetButton("Fire1"))
        {
            if (!isAttacking)
            {
                isAttacking = true;
                burstCooldownTimer = currentBurstCooldown;
                StartCoroutine(BurstFire());
            }
        }

        // Reset the attack state if the cooldown is active
        if (burstCooldownTimer <= 0)
        {
            isAttacking = false;
        }


    }

    // Shoots a bullet
    private void Shoot()
    {
        // Flash the muzzle flash
        StartCoroutine(Flash(0.04f));

        // Play the shoot sound
        SoundFXManager.instance.PlaySound(shootSound, transform, 1);

        // Create a bullet instance
        GameObject bullet = Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);

        // Configure the bullet's attributes
        bullet.GetComponent<PlayerBullet>().SetAttributes(bulletSpeed, lifeTime, damage);
    }

    // Coroutine for muzzle flash
    private IEnumerator Flash(float time)
    {
        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(time);
        muzzleFlash.SetActive(false);
    }

    // Coroutine for burst fire
    private IEnumerator BurstFire()
    {
        for (int i = 0; i < burstTimes; i++)
        {
            Shoot();
            yield return new WaitForSeconds(currentFireRate / TimeManager.instance.GetPlayerTimeScale());
        }
    }
}
