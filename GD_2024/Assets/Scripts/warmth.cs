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
    public float healthRefillRate = 3f;
    public float heatRefillRate = 5f;

    private ThirdPersonMovement thirdPM;
    private bool hasPlayedWarning = false; // To ensure sound only plays once
    public AudioSource warningSound; // Reference to the sound effect
    public GameObject tooCold;
    public GameObject game;
   

    void Start()
    {
        currentTemp = maxTemp;
        thirdPM = GetComponent<ThirdPersonMovement>();
        if (tooCold != null)
        {
            tooCold.SetActive(false);
           
        }
    }


    private void Update()
    {
        // Decrease oxygen over time
        DepleteHeat();
        UpdateTempUI();
        AdjustSpeed();

        CheckAndPlayWarningSound();

        // Check if oxygen is depleted
        if (currentTemp <= 0)
        {
            TriggerGameOver();
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
    private void AdjustSpeed()
    {
        if (thirdPM != null)
        {
            // Reduce speed when warmth is below half
            if (currentTemp < maxTemp / 2)
            {
                thirdPM.speed = 20f; // Set a slower speed
            }
            else
            {
                thirdPM.speed = 25f; // Reset to default speed
            }

            if(currentTemp < maxTemp / 4)
            {
                thirdPM.speed = 13f;
            }
            else
            {
                thirdPM.speed = 25f;
            }
        }

    }
    private void CheckAndPlayWarningSound()
    {
        if (currentTemp < maxTemp / 4 && !hasPlayedWarning)
        {
            hasPlayedWarning = true;
            if (warningSound != null)
            {
                warningSound.Play(); // Play the warning sound
            }
            else
            {
                Debug.LogWarning("Warning sound not assigned!");
            }
        }
        else if (currentTemp >= maxTemp / 4)
        {
            hasPlayedWarning = false; // Reset the flag when warmth goes back above half
        }
    }
    private void TriggerGameOver()
    {
        // Pause the game
        Time.timeScale = 0;

        // Activate the "You Lose" canvas
        if (tooCold != null)
        {
            tooCold.SetActive(true);
            game.SetActive(false);
            currentTemp = maxTemp;


        }
        else
        {
            Debug.LogWarning("You Lose Canvas is not assigned!");
        }

        Debug.Log("Too cold, you lose!");
    }
    void OnControllerColliderHit(ControllerColliderHit collision)
        {
            ThirdPersonMovement thirdPM = gameObject.GetComponent<ThirdPersonMovement>();
            // Check if the player is colliding with an air pocket
            if (collision.gameObject.tag == "pod")
            {
                Debug.Log("Heat Up ");
                // Increase oxygen level by the refill rate
                currentTemp += heatRefillRate * Time.deltaTime;
                currentTemp = Mathf.Clamp(currentTemp, 0, maxTemp);
                thirdPM.playerHealth = thirdPM.playerHealth + healthRefillRate * Time.deltaTime;
                thirdPM.healthSlider.value = thirdPM.playerHealth;
            }
        }
    }

