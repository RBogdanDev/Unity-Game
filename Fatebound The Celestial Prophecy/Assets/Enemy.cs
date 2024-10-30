using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float health, maximumHealth;
    public string DamageType;

    public float Health => health;
    public float MaximumHealth => maximumHealth;
    //public string DT => DamageType;

    // Start is called before the first frame update
    void Start()
    {
        maximumHealth = 40;
        health = maximumHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (health > maximumHealth)
        {
            health = maximumHealth;
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
