using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using System;

public class Player : MonoBehaviour, IDamageable
{
    public float movespeed = 5;
    private Rigidbody2D rb;
    private int Level, currentXP, maximumXP;
    private float health, maximumHealth;
    public UnityEngine.UI.Image healthBar;

    public float Health => health;
    public float MaximumHealth => maximumHealth;

    void Start()
    {
        maximumHealth = 100;
        health = maximumHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(horizontal, vertical) * movespeed;
        rb.velocity = movement;
        
        health = Mathf.Clamp(health, 0, maximumHealth);
    }

    private void OnEnable()
    {
        XPManager.Instance.onXPChange += HandleXPChange;
    }

    private void OnDisable()
    {
        XPManager.Instance.onXPChange -= HandleXPChange;
    }

    private void HandleXPChange(int newXP)
    {
        currentXP += newXP;
        if (currentXP >= maximumXP)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        maximumHealth += 20;
        health = maximumHealth;
        maximumXP += 200;
        currentXP = 0;
        Level += 1;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        healthBar.fillAmount = Mathf.Clamp(Health / MaximumHealth, 0, 1);
    }
}
