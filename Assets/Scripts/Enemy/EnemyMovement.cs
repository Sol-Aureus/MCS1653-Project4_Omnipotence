using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Rigidbody rb;

    private Transform player;

    private enum EnemyState
    {
        Attack,
        Chase
    }

    private EnemyState enemyState;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the enemy state
        enemyState = EnemyState.Chase;

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is within the enemy's line of sight
        if (Physics.Linecast(transform.position, player.position, out RaycastHit hit))
        {
            // If the enemy can see the player, set the enemy state to attack
            if (hit.transform.CompareTag("Player"))
            {
                // Set the enemy state to attack
                enemyState = EnemyState.Attack;
            }
            else
            {
                // Set the enemy state to chase
                enemyState = EnemyState.Chase;
            }
        }

        // Check if the enemy is in the attack or chase state
        switch (enemyState)
        {
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Chase:
                Chase();
                break;
            default:
                // Throws an error if the enemy state is invalid
                Debug.LogError("Invalid enemy state");
                break;
        }
    }

    // Method to handle the attack state
    private void Attack()
    {
        // Call the attack method from the enemy attack script
    }

    // Method to handle the chase state
    private void Chase()
    {
        // Move towards the player
    }
}
