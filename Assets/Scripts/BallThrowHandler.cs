using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BallThrowHandler : MonoBehaviour
{
    public XRGrabInteractable grabInteractable;  // Reference to the XR Grab component
    public ScoreManager scoreManager;            // Reference to the ScoreManager

    private void Start()
    {
        // Check that XR Grab Interactable is assigned, otherwise find it on this GameObject
        if (grabInteractable == null)
        {
            grabInteractable = GetComponent<XRGrabInteractable>();
        }

        // Subscribe to the OnSelectExited event (when the player releases the ball)
        grabInteractable.selectExited.AddListener(OnReleaseBall);
    }

    // Method called when the ball is released
    private void OnReleaseBall(SelectExitEventArgs args)
    {
        Debug.Log("Ball Released");
        // Register the throw in the ScoreManager
        scoreManager.RegisterThrow();
    }

    private void OnDestroy()
    {
        // Unsubscribe to avoid memory leaks
        grabInteractable.selectExited.RemoveListener(OnReleaseBall);
    }
}
