using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    
    public GameObject checkpoint1;
    private float checkDis;
    public TMP_Text checkpoint1Txt;
    
    public GameObject checkpoint2;
    private float checkDis2;
    public TMP_Text checkpoint2Txt;
    
    //Player Stats
    public int playerHealth = 100;
    public Slider healthSlider;
    public int wormDmgDone = 1;
    


    void Start()
    {
        checkpoint2.SetActive(false);
        healthSlider.maxValue = playerHealth;
        healthSlider.value = playerHealth;
    }

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
        Checkpoint();

    }
    
    void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            lizardPlayer.height = crouchHeight;
        }

        if (Input.GetKeyUp(KeyCode.C))
        {
            lizardPlayer.height = 11f;
        }
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag=="Enemy")
        {
            playerHealth = playerHealth - wormDmgDone;
            healthSlider.value = playerHealth;
            
        }
        if (playerHealth == 0)
        {
           //Add UI for dead player and turn game off 
            Debug.Log("Player Died");
        }
    }

    void Checkpoint()
    {
        checkDis = (checkpoint1.transform.position - lizardPlayer.transform.position).magnitude;
        checkpoint1Txt.text = "Distance from Checkpoint:" + checkDis;
        if (checkDis <= 6)
        {
           checkpoint1.SetActive(false);
            
        }
        if (checkpoint1.activeSelf.Equals(false))
        {
           checkpoint1Txt.gameObject.SetActive(false);
            checkpoint2.SetActive(true);
            Debug.Log("chkpoint2_on");
            checkDis2 = (checkpoint2.transform.position - lizardPlayer.transform.position).magnitude;
            checkpoint2Txt.text = "Distance from Checkpoint 2:" + checkDis2;
            
        }

        if (checkDis2 <= 6)
        {
            checkpoint2.SetActive(false);
        }
        
    }
}
