﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyFollow : MonoBehaviour
{

    public Transform target;
    float moveSpeed = 1f;
    float maxDist = 5;
    float minDist = 2.5f;
    private CharacterController controller;
    private float verticalVelocity;
    private Animator animator;
    private float gravity = 6f;
    Vector3 currentPos;
    int attackType;
    float attackTimer;
   




    // Start is called before the first frame update
    void Start()
    {
      //  target = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>();
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
       


    }

    // Update is called once per frame
    void Update()
    {
    //   if (healthBar.value <= 0) return;

       GravityForcing();

        WarriorIsAlive();
       // attackType = Random.Range(0, 11);

    }

    void GravityForcing()
    {
        Vector3 gravityVector = Vector3.zero;

        if (!controller.isGrounded)
        {
            gravityVector.y -= gravity;
        }
        controller.Move(gravityVector * Time.deltaTime);

        currentPos = transform.position;



    }
    void EnemyMove()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance < maxDist && distance > minDist)
        {
            transform.LookAt(target);
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
            animator.SetFloat("MovementSpeed", 0.5f);
        }
        else
        {
            transform.position = currentPos;
            animator.SetFloat("MovementSpeed", 0);
        }
    }
    void EnemyAttack()
    {

        float distance = Vector3.Distance(transform.position, target.position);



        if (distance < minDist)
        {
            attackTimer++;
            if (attackTimer > 80)
            {
             //   attackType = Random.Range(0, 5);
                attackTimer = 0;
            } 
        
            
           // Debug.Log(attackTimer);
         //   if (!animator.GetBool("isAttacking"))
         //   {
                animator.SetFloat("AttackType",attackType);
             
                 animator.SetBool("isAttacking", true);
                
          //  }
           

        }
        else
        {
            animator.SetBool("isAttacking", false);
        }


         
        
    }
    void WarriorIsAlive()
    {

        if (animator.GetBool("isDead") == false)
        {
            EnemyMove();

            EnemyAttack();
        }
    }
}
