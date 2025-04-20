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

    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        MyInput();
        SpeedControl();

        if (Input.GetButtonDown("Jump"))
        {
            moveSpeed /= 3;
            rb.drag *= 2;
        }
        if (Input.GetButtonUp("Jump"))
        {
            moveSpeed *= 3;
            rb.drag /= 2;
        }
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
        rb.AddForce(moveDirection.normalized * moveSpeed * 100, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
}
