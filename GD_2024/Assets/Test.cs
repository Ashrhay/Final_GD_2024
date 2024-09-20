using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public float movespeed = 5f;
    public Transform orientation;
    public float vertmove;
    public float horizmove;
    private Vector3 moveDirection;
    public Rigidbody player;
    public float playerHeight;
    public LayerMask whatIsGround;
    private bool grounded;
    public float GroundDrag;

    public float crouchHeight = 1f;
    public float jumpForce;
    
    private bool ReadyToJump;

    void Start()
    {
        player = GetComponent<Rigidbody>();
        player.freezeRotation = true;
    }

    void Update()
    {
        horizmove = Input.GetAxis("Horizontal");
        vertmove = Input.GetAxis("Vertical");
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);
        if (grounded)
            player.drag = GroundDrag;
        else
        {
            GroundDrag = 0;
        }

        MovePlayer();
        Crouch();
        Jump();
    }

    void MovePlayer()
    {
        moveDirection = orientation.forward * vertmove + orientation.right * horizmove;
        player.AddForce(moveDirection.normalized * movespeed, ForceMode.Force);
    }

    void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            playerHeight = crouchHeight;
        }

        if (Input.GetKeyUp(KeyCode.C))
        {
            playerHeight = playerHeight;
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.velocity = new Vector3(player.velocity.x, 0f, player.velocity.z);
            player.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }
    
}
   
    

