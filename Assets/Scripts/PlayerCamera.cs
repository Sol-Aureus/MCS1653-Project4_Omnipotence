using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform orientation;

    [Header("Attributes")]
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;

    private float rotationX;
    private float rotationY;


    // Start is called before the first frame update
    void Start()
    {
        // Prevents the cursor from being visible and allows for free movement of the camera
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * sensX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensY * Time.deltaTime;

        // Rotate the camera based on mouse input
        rotationX -= mouseY;
        rotationY += mouseX;

        // Prevent the camera from flipping upside down
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        // Apply the rotation to the camera
        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
        orientation.rotation = Quaternion.Euler(0, rotationY, 0);
    }
}
