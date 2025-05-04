using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangerMovement: MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody rb;

    [Header("Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    [SerializeField] private float maxOffset;

    private bool isMoving = true;
    private float moveDistance = 0;
    private Vector3 currentVel;
    private Vector3 currentAngle;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = new Vector3(Random.Range(-maxOffset, maxOffset), speed, Random.Range(-maxOffset, maxOffset));
        rb.angularVelocity = new Vector3(Random.Range(-maxOffset, maxOffset) * 10, Random.Range(-maxOffset, maxOffset) * 10, Random.Range(-maxOffset, maxOffset) * 10);

        currentVel = rb.velocity;
        currentAngle = rb.angularVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeManager.instance.IsTimeStopped() && isMoving)
        {
            currentVel = rb.velocity;
            currentAngle = rb.angularVelocity;
            isMoving = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.useGravity = false;
            return;
        }
        else if (TimeManager.instance.IsTimeStopped() && !isMoving)
        {
            return;
        }
        else if (!TimeManager.instance.IsTimeStopped() && !isMoving)
        {
            isMoving = true;
            rb.velocity = currentVel;
            rb.angularVelocity = currentAngle;
            rb.useGravity = true;
        }

        moveDistance += Time.deltaTime;

        // Check if the object is out of bounds
        if (moveDistance >= lifeTime)
        {
            // Destroy the object if it exceeds its lifetime
            Destroy(gameObject);
        }
    }
}
