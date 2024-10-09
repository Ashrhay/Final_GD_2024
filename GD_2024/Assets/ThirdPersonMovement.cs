using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController lizardPlayer;
    public float speed = 6f;
    private float turnSmooth = 0.1f;
    private float turnSmoothVelocity;
    public Transform cam;
    public float jumpSpeed = 2.0f;
    public float gravity = 10.0f;
    private Vector3 jumpDirection = Vector3.zero;
    private float crouchHeight = 6f;
    

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        //going between -1 and 1 using A&D
        float vertical = Input.GetAxisRaw("Vertical"); 
        //going between -1 and 1 using W&S
        if (lizardPlayer.isGrounded && Input.GetButton("Jump"))
        {
            jumpDirection.y = jumpSpeed;
        }
        jumpDirection.y -= gravity * Time.deltaTime;
        lizardPlayer.Move(jumpDirection * Time.deltaTime);
       
       
        Vector3 direction= new Vector3(horizontal,0f , vertical);

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z)*Mathf.Rad2Deg+ cam.eulerAngles.y;
            //atan2 returns the angle between the x axis and a vector starting at 0 and (x,y)
            //convert radians to degrees
            float angle =
               Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmooth);
           //smoothing numbers and angles in unity 
           transform.rotation = Quaternion.Euler(0f, angle+360, 0f);

           Vector3 moveDir = Quaternion.Euler(0f, angle, 0f)*Vector3.forward;
           lizardPlayer.Move(moveDir.normalized * (speed * Time.deltaTime));
          
        }

        Crouch();

    }
    
    void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            lizardPlayer.height = crouchHeight;
        }

        if (Input.GetKeyUp(KeyCode.C))
        {
            lizardPlayer.height = lizardPlayer.height;
            
        }
    }
}