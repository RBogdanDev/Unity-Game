using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public float movspeed = 5f;
    Rigidbody2D rb;
    UnsignedIntegerField level;
    float health;

    //se apeleaza la inceput
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    //aici update ul e fc la fiecare frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontal, vertical);
        movement *= movspeed;  // Înmulțim direcția cu viteza

        rb.velocity = movement;
    }

}
