
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;


public class BowlingBall : MonoBehaviour
{
    private Rigidbody rb;
    private XRGrabInteractable grabInteractable;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();

        rb.isKinematic = true;
        rb.useGravity = false;

        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        rb.isKinematic = false;
        rb.useGravity = true;
        ApplyThrowVelocity(args);
    }

    private void ApplyThrowVelocity(SelectExitEventArgs args)
    {
        // Use XRController to get the velocity and angular velocity
        if (args.interactorObject is XRController controller)
        {
            rb.velocity = controller.inputDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceVelocity, out Vector3 velocity) ? velocity : Vector3.zero;
            rb.angularVelocity = controller.inputDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceAngularVelocity, out Vector3 angularVelocity) ? angularVelocity : Vector3.zero;
        }
    }

    void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }
}
