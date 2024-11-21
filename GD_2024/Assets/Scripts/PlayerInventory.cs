using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    public float numberOfShrooms { get; private set; } // Track collected shrooms/ship parts
    public UnityEvent<PlayerInventory> OnshroomCollected;

    public int totalShipParts = 5; // Total number of ship parts to collect

    public void shroomCollected()
    {
        numberOfShrooms++;
        OnshroomCollected.Invoke(this);

        // Check if all parts have been collected
        if (numberOfShrooms >= totalShipParts)
        {
            LoadWinScene();
        }
    }

    private void LoadWinScene()
    {
        SceneManager.LoadScene("WinScene"); // Replace with the actual name of your win scene
    }
}
