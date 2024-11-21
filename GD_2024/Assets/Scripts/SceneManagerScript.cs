using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneManagerScript : MonoBehaviour
{
    private bool isPaused = false; // Tracks the pause state of the game

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Reset()
    {
        StartCoroutine(LoadSceneAsync());
    }

    private IEnumerator LoadSceneAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void TogglePauseGame()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0; // Pauses the game
        isPaused = true;
        Debug.Log("Game Paused");
    }

    private void ResumeGame()
    {
        Time.timeScale = 1; // Resumes the game
        isPaused = false;
        Debug.Log("Game Resumed");
    }
}
