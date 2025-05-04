using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Slider healthSlider;

    [Header("Attributes")]
    [SerializeField] private float maxHealth;

    private float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Damage the enemy
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Player took damage: " + damage);

        // Check if the enemy is dead
        if (currentHealth <= 0)
        {
            MenuManager.menus.Win();
        }

        // Update the health slider
        healthSlider.value = currentHealth / maxHealth;
    }
}
