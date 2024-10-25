using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinManager : MonoBehaviour
{
    public List<Pin> pins;  // List of all pins in the scene

    // Call this method to reset all pins after each frame
    public void ResetAllPins()
    {
        foreach (Pin pin in pins)
        {
            pin.ResetPin();  // Call the reset method for each pin
        }
    }
}
