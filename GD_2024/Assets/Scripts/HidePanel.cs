using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HidePanel : MonoBehaviour
{
    public string sceneName; // Name of the scene to load
    public float delay = 5f;   // Time in seconds before switching scenes
    public string sceneToLoad;  // Scene name
    public string targetObjectName; // The name of the GameObject to find in the new scene

    void Start()
    {
        // Start the coroutine to switch the scene after a delay
        StartCoroutine(SwitchSceneAfterDelay());
    }
   
    public void OpenObjectInScene()
    {
        StartCoroutine(LoadSceneAndFindObject());
    }

    private IEnumerator LoadSceneAndFindObject()
    {
        // Load the target scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);

        // Wait until the scene is loaded
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Find the GameObject in the newly loaded scene
        GameObject targetObject = GameObject.Find(targetObjectName);
        if (targetObject != null)
        {
            // Perform actions on the object
            targetObject.SetActive(true); // Example: activate it
        }
        else
        {
            Debug.LogWarning($"GameObject '{targetObjectName}' not found in scene '{sceneToLoad}'.");
        }
    }

    private IEnumerator SwitchSceneAfterDelay()
    {
        // Wait for the specified time
        yield return new WaitForSeconds(delay);

        // Load the specified scene
        SceneManager.LoadScene(sceneName);
    }
   
}
