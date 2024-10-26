using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public float movespeed = 5f;
    Rigidbody2D rb;
    int Level, currentXP, maximumXP;
    float health, maximumHealth;

    //Liniile astea 2 le adaugam cand o sa avem quest-uri
    //int XPAmount = 10;
    //XPManager.Instance.AddXP(XPAmount);

    // Start is called before the first frame update

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2 (horizontal, vertical) * movespeed;
        rb.velocity = movement;
    }

    private void onEnable(){
        //Subscribe event
        XPManager.Instance.onXPChange += HandleXPChange;
    }

    private void onDisable(){
        //Unsubscribe event
        XPManager.Instance.onXPChange -= HandleXPChange;
    }

    private void HandleXPChange(int newXP){
        currentXP += newXP;
        if(currentXP >= maximumXP){
            LevelUp();
        }
    }

    private void LevelUp(){
        maximumHealth += 20;
        health = maximumHealth;
        maximumXP += 200;
        currentXP = 0;
        Level += 1;
    }

}

