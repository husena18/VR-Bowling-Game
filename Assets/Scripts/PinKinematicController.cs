using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinKinematicController : MonoBehaviour
{
    private Rigidbody rb;
    private bool shouldActivate = false;

    void Start()
    {
        // Get the Rigidbody component and set the pins to be kinematic initially
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;  // Prevent the pins from falling at the start
    }

    void Update()
    {
        // Activate pin's physics when needed
        if (shouldActivate && rb.isKinematic)
        {
            rb.isKinematic = false; // Enable physics when activation is triggered
        }
    }

    // Method to activate the pins (e.g., after the ball is thrown)
    public void ActivatePins()
    {
        shouldActivate = true;
    }

    // Method to reset the pins back to kinematic state (e.g., when resetting after a frame)
    public void ResetPins()
    {
        shouldActivate = false;  // Reset the activation flag
        rb.velocity = Vector3.zero;        // Stop any movement
        rb.angularVelocity = Vector3.zero; // Stop any spin
        rb.isKinematic = true;             // Set back to kinematic to prevent falling
    }
}


