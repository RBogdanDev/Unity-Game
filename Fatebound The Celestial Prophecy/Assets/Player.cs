using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public float movspeed;
    float speed_x, speed_y;
    Rigidbody2D rb;
    UnsignedIntegerField level;
    float health;

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
}
