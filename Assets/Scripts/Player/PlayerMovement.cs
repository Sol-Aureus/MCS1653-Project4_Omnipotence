using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody rb;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed;

    private float currentMoveSpeed;

    private float horizontalInput;
    private float verticalInput;

    // Update is called once per frame
    void Update()
    {
        currentMoveSpeed = moveSpeed * TimeManager.instance.GetPlayerTimeScale();

        MyInput();
    }

    // FixedUpdate is called at a fixed interval and is independent of frame rate
    private void FixedUpdate()
    {
        MovePlayer();
    }

    // Gets the input from the player
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    // Moves the player based on the input
    private void MovePlayer()
    {
        // Calculate the velocity based on the input
        Vector3 velo = Vector3.zero;
        velo += transform.right * horizontalInput;
        velo += transform.forward * verticalInput;
        velo.Normalize(); // Normalize the vector to ensure consistent speed in all directions
        velo *= currentMoveSpeed;

        velo.y = rb.velocity.y; // Preserve the y velocity

        // Set the velocity of the rigidbody
        rb.velocity = velo;
    }
}
