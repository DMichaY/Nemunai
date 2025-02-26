using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaitoFight : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Animator animator;
    private Transform kaitoPivot;
    private bool isAttacking = false;
    
    void Start()
    {
        kaitoPivot = this.transform;
        animator = kaitoPivot.GetChild(0).GetComponent<Animator>();
    }

    void Update()
    {
        if (!isAttacking)
        {
            HandleMovement();
        }
        HandleAttacks();
    }

    void HandleMovement()
    {
        bool isMoving = false;
        if (Input.GetKey(KeyCode.A))
        {
            kaitoPivot.position += Vector3.forward * moveSpeed * Time.deltaTime;
            animator.SetBool("goLeft", true);
            isMoving = true;
        }
        else
        {
            animator.SetBool("goLeft", false);
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            kaitoPivot.position += Vector3.back * moveSpeed * Time.deltaTime;
            animator.SetBool("goRight", true);
            isMoving = true;
        }
        else
        {
            animator.SetBool("goRight", false);
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            animator.SetBool("crouch", true);
            return; // Evita que se mueva o ataque mientras está agachado
        }
        else
        {
            animator.SetBool("crouch", false);
        }

        if (!isMoving && !isAttacking)
        {
            animator.Play("idleFight");
        }
    }

    void HandleAttacks()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            StartCoroutine(PerformAttack("punchLeft"));
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            StartCoroutine(PerformAttack("punchRight"));
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            StartCoroutine(PerformAttack("kickLeft"));
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(PerformAttack("kickRight"));
        }
    }

    IEnumerator PerformAttack(string attackName)
    {
        isAttacking = true;
        animator.SetBool(attackName, true);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        animator.SetBool(attackName, false);
        isAttacking = false;
    }
}
