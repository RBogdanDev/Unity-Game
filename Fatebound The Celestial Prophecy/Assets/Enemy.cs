using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    private float health, maximumHealth;

    public float Health => health;
    public float MaximumHealth => maximumHealth;

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
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
