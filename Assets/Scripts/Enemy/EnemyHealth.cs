using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject healthCanvas;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private GameObject[] powerChangers;

    [Header("Attributes")]
    [SerializeField] private float maxHealth;
    [SerializeField] private bool isChanger;
    [SerializeField] private int power;

    private float currentHealth;
    private bool dead;
    private float spawnTime;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        dead = false;

        if (!isChanger)
        {
            healthCanvas.SetActive(false);
        }

        spawnTime = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTime -= Time.deltaTime;
    }


    // Damage the enemy
    public void TakeDamage(float damage)
    {
        if (spawnTime > 0)
        {
            return; // Prevent damage during spawn time
        }

        if (!isChanger)
        {
            // Enables the health bar
            healthCanvas.SetActive(true);
        }

        // Damages the enemy
        currentHealth -= damage;

        // Check if the enemy is dead
        if (currentHealth <= 0 && !dead)
        {
            dead = true;
            if (isChanger)
            {
                TimeManager.instance.SwitchPower(power);
                Debug.Log("Power Changer: " + power);
            }
            else
            {
                Instantiate(powerChangers[Random.Range(0, powerChangers.Length)], transform.position, Quaternion.identity);
                EnemySpawner.main.DecreaseEnemies();
            }

            Destroy(gameObject);
        }

        if (!isChanger)
        {
            // Update the health slider
            healthSlider.value = currentHealth / maxHealth;
        }
    }
}
