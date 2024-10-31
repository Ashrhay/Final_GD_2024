using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{
    public Light[] roomLights;

    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ToggleLights(true);  
        }
    }

    // When the player leaves the room, turn the lights off
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ToggleLights(false);  
        }
    }

    
    void ToggleLights(bool state)
    {
        foreach (Light light in roomLights)
        {
            light.enabled = state;  // Turn light on or off based on the state
        }
    }
}
