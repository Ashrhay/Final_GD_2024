using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class HidePanel : MonoBehaviour
{
    public GameObject loadingScreen;

    public GameObject Game;
    // Update is called once per frame
    void FixedUpdate()
    {
        StartCoroutine(ClosedPanelDelayed());
    }

    private IEnumerator ClosedPanelDelayed()
    {
        if (Game.activeSelf.Equals(false))
        {
            yield return new WaitForSeconds(3f);
            loadingScreen.SetActive(false);
            Game.SetActive(true); 
        }
        
    }
}
