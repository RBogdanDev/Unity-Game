using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject player;
    private float movement = 4, deptofview = 4, distance;
    private bool aggressive = false;

    public void ForceAggressive()
    {
        aggressive = true;
        Debug.Log("Enemy has become aggressive due to being attacked!");
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;
        Vector2 forwardDirection = transform.right;

        float angle = Vector2.Angle(forwardDirection, directionToPlayer);

        //verificam cat de departe este player-ul de inamic si daca e in fata sau in spatele lui
        if (distance < deptofview && angle < 90){
            aggressive = true;
        }
        if(aggressive){
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, movement * Time.deltaTime);
        }
        if(distance >= deptofview){
            aggressive = false;
        }
    }
}
