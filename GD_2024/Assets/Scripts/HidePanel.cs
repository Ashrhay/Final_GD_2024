using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HidePanel : MonoBehaviour
{
    public string sceneName; // Name of the scene to load
    public float delay = 5f;   // Time in seconds before switching scenes
   

    void Start()
    {
        // Start the coroutine to switch the scene after a delay
        StartCoroutine(SwitchSceneAfterDelay());
    }
    
    private IEnumerator SwitchSceneAfterDelay()
    {
        // Wait for the specified time
        yield return new WaitForSeconds(delay);

        // Load the specified scene
        SceneManager.LoadScene(sceneName);
    }
   
}
