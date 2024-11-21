using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneManagerScript : MonoBehaviour
{
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
}
