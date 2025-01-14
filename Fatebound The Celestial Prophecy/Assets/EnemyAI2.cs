using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI2 : MonoBehaviour
{
    public GameObject player;
    private AudioSource audioSource;
    public AudioClip walkClip;
    private Animator animator;
    private float movement = 4, deptofview, distance;
    private bool aggressive = false;
    private Enemy enemytype;

    public void ForceAggressive()
    {
        aggressive = true;
        Debug.Log("Enemy has become aggressive due to being attacked!");
    }
    void Start()
    {
        animator=GetComponent<Animator>();
        animator.Play("Idle");
        audioSource=GetComponent<AudioSource>();
        enemytype = GetComponent<Enemy>();
        if (enemytype != null)
        {
            // Setăm deptofview pe baza tipului de inamic
            switch (enemytype.type)
            {
                case Type.Melee:
                    deptofview = 4;
                    break;
                case Type.Projectile:
                    deptofview = 10;
                    break;
                case Type.Magic:
                    deptofview = 8;
                    break;
                default:
                    deptofview = 4;
                    break;
            }
        }
        else{
            deptofview = 4;
        }
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
            animator.SetFloat("AnimState",2);
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, movement * Time.deltaTime);
        }
        if(distance >= deptofview){
            aggressive = false;
            animator.Play("Idle");
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
