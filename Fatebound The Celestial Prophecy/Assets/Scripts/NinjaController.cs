using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaController : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        //la start pornim animatia default pentru idle
        animator = GetComponent<Animator>();
        animator.Play("Idle_animation");
    }

    void Update()
    {
        // detecteaza input-ul pentru miscare (WASD)
        float moveX = Input.GetAxis("Horizontal"); // AD
        float moveY = Input.GetAxis("Vertical");   // WS

        //setam parametrii din animator(combinatii de 1 si -1)
        animator.SetFloat("moveX", moveX);
        animator.SetFloat("moveY", moveY);

        // este evident ca pentru a trece din starea de idle la running va trebui sa se miste pe cel putin una din cele doua axe, adica produsul moveX*moveY sa fie diferit de 0
        bool isRunning = moveX != 0 || moveY != 0;
        animator.SetBool("isRunning", isRunning);

        // daca se apasa Q
        if (Input.GetKeyDown(KeyCode.Q))
        {
            animator.SetBool("isCrouch", true);
        }
        //daca se elibereaza tasta Q
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            animator.SetBool("isCrouch", false);
        }

        // click stanga pentru atac
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("attack");
        }
    }
}
