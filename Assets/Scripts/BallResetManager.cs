using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallResetManager : MonoBehaviour
{
    public Transform ballStartPosition; // Position where the ball resets
    public Transform ball;              // Reference to the ball's transform
    public Rigidbody ballRigidbody;     // Reference to the ball's Rigidbody
    public float resetDelay = 2f;       // Delay before resetting the ball
    public float stopThreshold = 0.1f;  // Velocity threshold to consider the ball stopped
    private bool isThrown = false;      // Whether the ball has been thrown

    private int trialsLeft = 2;         // Number of trials per frame (standard bowling)

    void Start()
    {
        // Ensure references to the ball and Rigidbody are assigned
        if (ball == null)
        {
            ball = GameObject.FindWithTag("Ball").transform;
        }

        if (ballRigidbody == null)
        {
            ballRigidbody = ball.GetComponent<Rigidbody>();
        }
    }

    void Update()
    {
        // Check if the ball has been thrown and has stopped moving
        if (isThrown && ballRigidbody.velocity.magnitude < stopThreshold)
        {
            StartCoroutine(ResetBallAfterDelay());
        }
    }

    // Call this when the ball is thrown
    public void OnBallThrown()
    {
        isThrown = true;
    }

    // Coroutine to reset the ball after a delay
    IEnumerator ResetBallAfterDelay()
    {
        yield return new WaitForSeconds(resetDelay);

        // Reset the ball position only if there are trials left
        if (trialsLeft > 0)
        {
            ResetBall();
            trialsLeft--; // Reduce trial count
        }
        else
        {
            // If no trials left, end frame and potentially reset the pins
            EndFrame();
        }
    }

    // Reset the ball to its original position
    private void ResetBall()
    {
        // Reset the position and stop the ball's movement
        ball.position = ballStartPosition.position;
        ballRigidbody.velocity = Vector3.zero;
        ballRigidbody.angularVelocity = Vector3.zero;

        isThrown = false; // Ball is now reset and ready for another throw
    }

    // End the frame and reset pins (or start a new frame)
    private void EndFrame()
    {
        trialsLeft = 2; // Reset the number of trials for the next frame
        // Logic to reset the pins can be added here
    }
}
