using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShooting : MonoBehaviour
{
    public GameObject Projectile;
    public Transform ProjectilePos;
    private GameObject player;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        float distance = Vector2.Distance(transform.position, player.transform.position);
        Debug.Log(distance);

        if(distance >= 3 && distance <= 10){

            timer += Time.deltaTime;
            if(timer > 2){
            timer = 0;
            shoot();
            }
        }
    }

    void shoot(){
        Instantiate(Projectile, ProjectilePos.position, Quaternion.identity);
    }
}