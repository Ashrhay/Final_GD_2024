using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSound : MonoBehaviour
{
    public AudioSource jumpSound; 

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            jumpSound.enabled = true; 
        }
        else
        {
            jumpSound.enabled = false; 
        }
    }
}
