using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject player;
    private AudioSource audioSource;
    public AudioClip walkClip;
    private Animator animator;
    private float movement = 4, deptofview = 4, distance;
    private bool aggressive = false;

    public void ForceAggressive()
    {
        aggressive = true;
        Debug.Log("Enemy has become aggressive due to being attacked!");
    }
    void Start()
    {
        animator=GetComponent<Animator>();
        animator.Play("idle");
        audioSource=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player!=null)//daca player ul nu a fost distrus deja
        {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;
        Vector2 forwardDirection = transform.right;

        float angle = Vector2.Angle(forwardDirection, directionToPlayer);

        //verificam cat de departe este player-ul de inamic si daca e in fata sau in spatele lui
        if (distance < deptofview && angle < 180){
            aggressive = true;
        }
        if(aggressive){
 
            if (player.transform.position.x < this.transform.position.x) // daca player-ul este in stanga enemy-ului
        {
              Vector3 scale = this.transform.localScale; 
              scale.x = Mathf.Abs(scale.x) * -1; // Inversăm direcția pe axa X
              this.transform.localScale = scale; 
               }
             else // daca player-ul este in dreapta enemy-ului
             {
              Vector3 scale = this.transform.localScale;
              scale.x = Mathf.Abs(scale.x); // Ne asigurăm că direcția pe X este pozitivă
              this.transform.localScale = scale;
        }
            animator.SetBool("isRunning",true);
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, movement * Time.deltaTime);
        }
        if(distance >= deptofview){
            aggressive = false;
            animator.SetBool("isRunning",false);
        }
        }
        else{
            Debug.LogWarning("Player has been eliminated!");
        }
    }
    public void PlayWalkSoundEnemy()
    {
        audioSource.PlayOneShot(walkClip);
    }
}
