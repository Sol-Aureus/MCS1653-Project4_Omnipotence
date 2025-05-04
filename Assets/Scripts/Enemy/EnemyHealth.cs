using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject healthCanvas;
    [SerializeField] private Slider healthSlider;

    [Header("Attributes")]
    [SerializeField] private float maxHealth;

    private float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthCanvas.SetActive(false);
    }

    // Damage the enemy
    public void TakeDamage(float damage)
    {
        // Enables the health bar
        healthCanvas.SetActive(true);

        // Damages the enemy
        currentHealth -= damage;
        Debug.Log("Enemy took damage: " + damage);

        // Check if the enemy is dead
        if (currentHealth <= 0)
        {
            TimeManager.instance.SwitchPower(Random.Range(0, 3));
            Destroy(gameObject);
        }

        // Update the health slider
        healthSlider.value = currentHealth / maxHealth;
    }
}
