using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public float movspeed;
    float speed_x, speed_y;
    Rigidbody2D rb;
    int Level, currentXP, maximumXP, maximumHealth;
    float health;

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
        speed_x = Input.GetAxis("Horizontal") * movspeed;
        speed_y = Input.GetAxis("Vertical") * movspeed;
        rb.velocity = new Vector2(speed_x, speed_y);
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