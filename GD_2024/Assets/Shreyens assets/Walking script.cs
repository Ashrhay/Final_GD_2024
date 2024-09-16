using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walkingscript : MonoBehaviour
{
    public CharacterController PlayerHeight;
    public float movespeed = 5f;
     public float gravity = -9.81f;
     public  float jumpHeight = 2f;
    public  Vector3 velocity;
    public float normalHeight;
    public float crouchHeight;
    



    void Update()
    {
        float horizmove = Input.GetAxisRaw("Horizontal")*movespeed*Time.fixedTime;
        float vertmove = Input.GetAxisRaw("Vertical")*movespeed*Time.fixedTime;
        Vector3 direction = new Vector3(horizmove, 0f, vertmove).normalized;
        Jump();
        Crouch();
    }

    void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            PlayerHeight.height = crouchHeight;
        }

        if (Input.GetKeyUp(KeyCode.C))
        {
            PlayerHeight.height = normalHeight;
        }
    }
}
