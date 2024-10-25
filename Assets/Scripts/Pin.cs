using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    private bool isKnockedDown = false;        // Track if the pin is already knocked down
    public ScoreManager scoreManager;          // Reference to the ScoreManager script
    private Rigidbody rb;                      // Rigidbody of the pin

    // The threshold angle to consider the pin knocked down
    private const float KnockdownAngleThreshold = 45f;

    void Start()
    {
        // Get the Rigidbody component attached to the pin
        rb = GetComponent<Rigidbody>();

        // Find the ScoreManager in the scene if it's not assigned
        if (scoreManager == null)
        {
            scoreManager = FindObjectOfType<ScoreManager>();
        }
    }

    void Update()
    {
        // Check if the pin has been knocked over (based on its tilt angle)
        if (!isKnockedDown && IsPinKnockedDown())
        {
            isKnockedDown = true;
            scoreManager.AddScore(1);  // Add score for knocked down pin
        }
    }

    // This method checks if the pin is knocked down by checking its tilt
    private bool IsPinKnockedDown()
    {
        // Use localEulerAngles to properly account for rotation changes
        float xRotation = Mathf.Abs(transform.localEulerAngles.x);
        float zRotation = Mathf.Abs(transform.localEulerAngles.z);

        // Normalize the angles to deal with rotations past 180 degrees
        if (xRotation > 180f) xRotation = 360f - xRotation;
        if (zRotation > 180f) zRotation = 360f - zRotation;

        // If the pin's rotation on the x or z axis exceeds the threshold, it's considered knocked down
        return xRotation > KnockdownAngleThreshold || zRotation > KnockdownAngleThreshold;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision object has the "Ball" tag
        if (collision.gameObject.CompareTag("Ball"))
        {
            Debug.Log("Ball hit the pin!");
        }
    }

    // Method to reset the pin (for the next frame)
    public void ResetPin()
    {
        isKnockedDown = false;
        rb.velocity = Vector3.zero;       // Stop any movement
        rb.angularVelocity = Vector3.zero; // Stop any spin
        rb.isKinematic = true;            // Temporarily disable physics to reset position

        // Reset the rotation and position to default or a predefined starting point
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0); // Keep the Y rotation intact
        transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z); // Adjust height appropriately

        rb.isKinematic = false;           // Re-enable physics
    }
}

