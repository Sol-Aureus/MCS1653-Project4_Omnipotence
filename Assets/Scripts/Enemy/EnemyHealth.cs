using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
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
        Debug.Log("Enemy took damage: " + damage);

        // Check if the enemy is dead
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
