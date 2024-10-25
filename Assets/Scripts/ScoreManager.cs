using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int totalScore = 0;          // Total score across all frames
    private int pinsKnockedDown = 0;    // Pins knocked down in the current frame
    private int throwsRemaining = 2;    // Number of throws per frame
    private int currentFrame = 1;       // Current frame
    public int maxFrames = 10;          // Total number of frames in the game

    public PinManager pinManager;       // Reference to PinManager
    public TextMeshProUGUI scoreText;   // Reference to TextMeshPro UI element for displaying score

    void Start()
    {
        UpdateScoreUI();  // Initialize score UI
    }

    // Add score based on how many pins were knocked down
    public void AddScore(int pins)
    {
        pinsKnockedDown += pins;
        totalScore += pins;
        UpdateScoreUI();  // Update the score on the UI after adding points

        // Handle strike (all pins knocked down in one throw)
        if (pinsKnockedDown == 10 && throwsRemaining == 2)
        {
            Debug.Log("Strike!");
            ResetFrame();
        }
    }

    // Register a throw (reduce the number of throws per frame)
    public void RegisterThrow()
    {
        throwsRemaining--;

        if (throwsRemaining <= 0 || pinsKnockedDown == 10)
        {
            ResetFrame();
        }
    }

    // Reset the frame after two throws or a strike
    public void ResetFrame()
    {
        pinsKnockedDown = 0;
        throwsRemaining = 2;
        currentFrame++;

        // Reset the pins using PinManager
        if (pinManager != null)
        {
            pinManager.ResetAllPins();
        }

        // Check if the game has ended (after max frames)
        if (currentFrame > maxFrames)
        {
            EndGame();
        }
    }

    // Update the score UI display
    public void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + totalScore;  // Update the score display
        }
    }

    // End game and display final score
    public void EndGame()
    {
        Debug.Log("Game Over! Final Score: " + totalScore);

        // Optionally update UI to show final score
        if (scoreText != null)
        {
            scoreText.text = "Game Over! Final Score: " + totalScore;
        }
    }
}
