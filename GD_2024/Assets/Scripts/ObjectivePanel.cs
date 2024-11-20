using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectivePanel : MonoBehaviour
{
    public GameObject Objectivepanel;

    void Start()
    {
        if (Objectivepanel == null)
        {
            Debug.LogError("Objectivepanel is not assigned in the Inspector.");
        }
        else
        {
            Objectivepanel.SetActive(false); // Ensure the panel starts inactive
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (Objectivepanel != null)
            {
                // Toggle the active state of the panel
                bool isActive = Objectivepanel.activeSelf;
                Objectivepanel.SetActive(!isActive);

                // Log the new state for debugging
                Debug.Log("Panel is now " + (!isActive ? "active" : "inactive"));
            }
        }
    }
}
