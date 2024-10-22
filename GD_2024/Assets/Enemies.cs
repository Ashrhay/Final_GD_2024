using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;



public class Enemies : MonoBehaviour
{
   
    
    public Transform player;
    public NavMeshAgent worm;
    public LayerMask whatIsGround, whatIsPlayer;
    
    //Worm Patrolling
    public Vector3 walkpoint;
    private bool walkpointSet;
    public float walkpointRange;
    
    //Worm Attacking
    public float timeBetweenAttacks;
    private bool AlreadyAttacked;
    
    //States
    public float sightRange;
    public float attackRange;
    public bool playerInAttackRange;
    public bool playerInSightRange;

    void Update()
    {
        //Check if enemy is in player sight and in attack range by checking for colliders in a spherical area 
        //If doesnt work, remove worm from transform.position
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        
        if((playerInSightRange && playerInAttackRange)==false)
        {
            Patrolling();
        }

        if ((playerInSightRange ) && playerInAttackRange == false)
        {
            ChasePlayer();
        }

        if (playerInSightRange && playerInAttackRange)
        {
            AttackPlayer();
        }
    }

    void Awake()
    {
        player = GameObject.Find("LizardPlayer").transform;
        worm = GetComponent<NavMeshAgent>();
        
    }

   

    void Patrolling()
    {
        if (walkpointSet==false)
        {
            SearchForWalkpoint();
            worm.SetDestination(walkpoint);

        }

        Vector3 distToWalkpoint = transform.position - walkpoint;

        if (distToWalkpoint.magnitude <= 1)
        {
            walkpointSet = false;
        }
    }

    void SearchForWalkpoint()
    {
        //Set Random X and Z values as y values will send worm flying 
        float randomZ = Random.Range(-walkpointRange, walkpointRange);
        float randomX = Random.Range(-walkpointRange, walkpointRange);
        walkpoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        
        //Checking if walkpoint made is on the ground
        if (Physics.Raycast(walkpoint, -transform.up, 2f, whatIsGround))
        {
            walkpointSet = true;
        }
    }
    

    void ChasePlayer()
    {
        worm.SetDestination(player.position);
    }

    void AttackPlayer()
    {
        //Once enemy is by player, stop moving
        worm.SetDestination(transform.position);
        transform.LookAt(player);

        if (AlreadyAttacked == false)
        {
            //Attack code
            //
            //
            //
            
            AlreadyAttacked = true;
            Invoke(nameof(ResetAttack),timeBetweenAttacks);
        }
    }

    void ResetAttack()
    {
        AlreadyAttacked = false;
    }

    void TakeDamage()
    {
        
    }
    
}
