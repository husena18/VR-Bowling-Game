using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallVelocityLimiter : MonoBehaviour
{
    [Header("Velocity Control")]
    public float maxSpeed = 5.0f;      // Maximum allowed speed for the ball
    public float maxAngularVelocity = 10.0f; // Maximum allowed angular (spin) speed

    private Rigidbody rb;

    void OnEnable()
    {
        // Get the Rigidbody component on the ball when enabled
        rb = GetComponent<Rigidbody>();

        // Check if the Rigidbody exists to prevent NullReferenceException
        if (rb != null)
        {
            // Optional: Reset velocity when the ball is picked up or respawned
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        else
        {
            Debug.LogError("Rigidbody component is missing on the ball.");
        }
    }

    void Start()
    {
        // Get the Rigidbody component on the ball
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Ensure Rigidbody is assigned
        if (rb != null)
        {
            // Limit linear velocity
            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }

            // Limit angular (spin) velocity
            if (rb.angularVelocity.magnitude > maxAngularVelocity)
            {
                rb.angularVelocity = rb.angularVelocity.normalized * maxAngularVelocity;
            }
        }
        else
        {
            Debug.LogError("Rigidbody component is missing on the ball.");
        }
    }
}
