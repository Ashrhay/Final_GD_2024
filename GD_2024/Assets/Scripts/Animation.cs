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
            if (Input.GetKey(KeyCode.W))
            {
                Debug.Log("Triggering walk animation");
                mAnimator.SetTrigger("Walk");
                
            }
            else
            {
                mAnimator.SetTrigger("Idle");
                
            }


            if (Input.GetKey(KeyCode.D))
            {
                Debug.Log("Triggering walk animation");
                mAnimator.SetTrigger("Walk");
                
            }
          
            if (Input.GetKey(KeyCode.S))
            {
                Debug.Log("Triggering walk animation");
                mAnimator.SetTrigger("Walk");
                
            }
          

            if (Input.GetKey(KeyCode.A))
            {
                Debug.Log("Triggering walk animation");
                mAnimator.SetTrigger("Walk");
               
            }
           

            if (Input.GetKey(KeyCode.Space))
            {
                Debug.Log("Triggering jump animation");           
                mAnimator.SetTrigger("Jump");
            }
            
        }
        else
        {
            mAnimator.SetTrigger("Idle");
        }
    }
}
