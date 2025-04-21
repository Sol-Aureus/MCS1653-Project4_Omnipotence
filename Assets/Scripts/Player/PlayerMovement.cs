using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform orientation;
    [SerializeField] private Rigidbody rb;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed;

    private float currentMoveSpeed;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveDirection;

    // Update is called once per frame
    void Update()
    {
        currentMoveSpeed = moveSpeed * TimeManager.instance.GetPlayerTimeScale();

        MyInput();
        SpeedControl();
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
        // Calculate the move direction based on the orientation of the camera
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // Adds force to the player in the move direction
        rb.AddForce(moveDirection.normalized * currentMoveSpeed * 100, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (flatVel.magnitude > currentMoveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * currentMoveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
}
