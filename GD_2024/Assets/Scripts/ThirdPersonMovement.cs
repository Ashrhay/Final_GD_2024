using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ThirdPersonMovement : MonoBehaviour
{
    //Character 
    public CharacterController lizardPlayer;
    public float speed = 6f;
    private float turnSmooth = 0.1f;
    private float turnSmoothVelocity;
    public Transform cam;
    public float jumpSpeed = 2.0f;
    public float gravity = 10.0f;
    private Vector3 jumpDirection = Vector3.zero;
    private float crouchHeight = 6f;
    
    //Checkpoints
    public GameObject checkpoint1;
    private float checkDis;
    public TMP_Text checkpoint1Txt;
    
    public GameObject checkpoint2;
    private float checkDis2;
    public TMP_Text checkpoint2Txt;

    public GameObject checkpoint3;
    private float checkDis3;
    public TMP_Text checkpoint3Txt;

    public GameObject checkpoint4;
    private float checkDis4;
    public TMP_Text checkpoint4Txt;
    
    //Player Stats
    public float playerHealth = 100;
    public float maxHealth = 100;
    public Slider healthSlider;
    public float wormDmgDone = 1;

    //Mushroom Health;
    public float mushroomHealValue = 20;
    public GameObject healEffect; //particle effect

    //UI
   
    


    void Start()
    {
        
        checkpoint1.SetActive(true);
        checkpoint2.SetActive(false);
        checkpoint3.SetActive(false);
        checkpoint4.SetActive(false);
        
        healthSlider.maxValue = playerHealth;
        healthSlider.value = playerHealth;
    }

   

    // Update is called once per frame
    void Update()
    {
        CharacterCenter();
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
    void CharacterCenter()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) ||Input.GetKey(KeyCode.A) || Input.GetKey(
                KeyCode.D))
        {
            lizardPlayer.center = new Vector3(1, (float)4, -1) ;
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            lizardPlayer.center = new Vector3(1, (float)5.2, -1) ;
        }
        
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
        if (hit.gameObject.tag == "Mushroom")
        {
            HealPlayer(mushroomHealValue);
            Instantiate(healEffect, hit.transform.position, Quaternion.identity);
            Destroy(hit.gameObject);
        }
        if (playerHealth == 0)
        {
            GameOver();
            Debug.Log("Player Died");
        }
    }
    void GameOver()
    {
        if (playerHealth == 0)
        {
            SceneManager.LoadScene("LoseScene");
        }
    }

    void HealPlayer(float healAmount)
    {
        playerHealth += healAmount;
        if (playerHealth > maxHealth)
        {
            playerHealth = maxHealth;
        }
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        healthSlider.value = playerHealth;
    }

    void Checkpoint()
    {
        if (checkpoint1.activeSelf.Equals(true) && checkpoint2.activeSelf.Equals(false) &&
            checkpoint3.activeSelf.Equals(false) && checkpoint4.activeSelf.Equals(false))
        {
            checkDis = (checkpoint1.transform.position - lizardPlayer.transform.position).magnitude;
            checkpoint1Txt.text = "Distance from Checkpoint:" + checkDis;
            
            if (checkDis <= 6)
            {
                checkpoint1.SetActive(false);
                checkpoint2.SetActive(true);

            }
        }

        if (checkpoint1.activeSelf.Equals(false) && checkpoint2.activeSelf.Equals(true) &&
            checkpoint3.activeSelf.Equals(false)
            && checkpoint4.activeSelf.Equals(false))
        {
            checkpoint1Txt.gameObject.SetActive(false);
            checkpoint2Txt.gameObject.SetActive(true);
            checkDis2 = (checkpoint2.transform.position - lizardPlayer.transform.position).magnitude;
            checkpoint2Txt.text = "Distance from Checkpoint 2:" + checkDis2;
            
            if (checkDis2 <= 6)
            {
                checkpoint2.SetActive(false);
                checkpoint3.SetActive(true);
            }
        }

        if (checkpoint1.activeSelf.Equals(false) && checkpoint2.activeSelf.Equals(false) &&
            checkpoint3.activeSelf.Equals(true)
            && checkpoint4.activeSelf.Equals(false))
        {
            checkpoint2Txt.gameObject.SetActive(false);

            checkpoint3Txt.gameObject.SetActive(true);
            checkDis3 = (checkpoint3.transform.position - lizardPlayer.transform.position).magnitude;
            checkpoint3Txt.text = "Distance from Checkpoint 3:" + checkDis3;

            if (checkDis3 <= 6)
            {
                checkpoint3.SetActive(false);
                checkpoint4.SetActive(true);

            }
        }

        if (checkpoint1.activeSelf.Equals(false) && checkpoint2.activeSelf.Equals(false) &&
            checkpoint3.activeSelf.Equals(false) && checkpoint4.activeSelf.Equals(true))
        {
            
            checkpoint3Txt.gameObject.SetActive(false);
            checkpoint4Txt.gameObject.SetActive(true);
            checkDis4 = (checkpoint4.transform.position - lizardPlayer.transform.position).magnitude;
            checkpoint4Txt.text = "Distance from Checkpoint 4:" + checkDis4;

            if (checkDis4 <= 6)
            {
                checkpoint4.SetActive(false);
            }
            
        }

        if (checkpoint1.activeSelf.Equals(false) && checkpoint2.activeSelf.Equals(false) &&
            checkpoint3.activeSelf.Equals(false) && checkpoint4.activeSelf.Equals(false))
        {
            Debug.Log("End");
            checkpoint4Txt.gameObject.SetActive(false);
        }
        
    }
}