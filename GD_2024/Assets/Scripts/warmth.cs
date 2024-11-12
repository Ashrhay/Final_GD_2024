using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class warmth : MonoBehaviour
{
    public Image heatBar;
    public float maxTemp;
    public float currentTemp;
    public float heatDepleateRate = 0.5f;
    public float heatRefillRate = 5f;
    void Start()
    {
        currentTemp = maxTemp;
    }

   private void Update()
    {
        // Decrease oxygen over time
        DepleteHeat();
        UpdateTempUI();
        
        // Check if oxygen is depleted
        if (currentTemp <= 0)
        {
            // Handle the case where oxygen runs out (e.g., game over)
            Debug.Log("Too cold, you Die");
        }
    }

    private void DepleteHeat()
    {
        // Reduce the oxygen level based on the depletion rate
        currentTemp -= heatDepleateRate * Time.deltaTime;
        // Ensure the oxygen level doesn't drop below zero
        currentTemp = Mathf.Clamp(currentTemp, 0, maxTemp);
    }

    private void UpdateTempUI()
    {
        
        heatBar.fillAmount = currentTemp / maxTemp;
    }

    void OnControllerColliderHit(ControllerColliderHit collision)
    {
        // Check if the player is colliding with an air pocket
        if (collision.gameObject.tag=="pod")
        {
            Debug.Log("Heat Up ");
            // Increase oxygen level by the refill rate
            currentTemp += heatRefillRate * Time.deltaTime;
            currentTemp = Mathf.Clamp(currentTemp, 0, maxTemp);
        }
    }
}
