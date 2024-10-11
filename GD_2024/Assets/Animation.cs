using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    private Animator mAnimator;
    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(mAnimator != null)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Debug.Log("Triggering walk animation");
                mAnimator.SetTrigger("Walk");
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                Debug.Log("Triggering walk animation");
                mAnimator.SetTrigger("Walk");
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                Debug.Log("Triggering walk animation");
                mAnimator.SetTrigger("Walk");

            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("Triggering walk animation");
                mAnimator.SetTrigger("Walk");
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Triggering jump animation");
                mAnimator.SetTrigger("Jump");
            }
        }
    }
}
