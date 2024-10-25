using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinCenterOfMass : MonoBehaviour
{
    public Vector3 customCenterOfMass = new Vector3(0, -0.5f, 0); // Adjust as needed

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.centerOfMass = customCenterOfMass;
        }
    }
}
