using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BallGrabAndThrow : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;  // XR interaction for grabbing
    public BallResetManager ballResetManager;     // Reference to the BallResetManager

    private void Start()
    {
        // Get the XRGrabInteractable component (already attached to the ball)
        grabInteractable = GetComponent<XRGrabInteractable>();

        // Hook into the release event (onSelectExited) to detect when the ball is released
        grabInteractable.onSelectExited.AddListener(OnBallReleased);
    }

    // This method will be called when the ball is released
    private void OnBallReleased(XRBaseInteractor interactor)
    {
        if (ballResetManager != null)
        {
            // Notify BallResetManager that the ball has been thrown
            ballResetManager.OnBallThrown();
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event to avoid memory leaks
        if (grabInteractable != null)
        {
            grabInteractable.onSelectExited.RemoveListener(OnBallReleased);
        }
    }
}
