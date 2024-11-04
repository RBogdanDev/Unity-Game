using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using System;

public class Player : MonoBehaviour, IDamageable
{
    private float movespeed = 5;
    private Rigidbody2D rb;
    private int Level, currentXP, maximumXP;
    private float health, maximumHealth;
    public UnityEngine.UI.Image healthBar;

    public float Health => health;
    public float MaximumHealth => maximumHealth;

    private DamageInfo attackMelee = new DamageInfo(20, Type.Melee, Response.Stagger, true);
    private bool isIntterupteble = true;

    public Transform AttackPoint;
    public float AttackRange = 0.5f;

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

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack(attackMelee);
        }
    }

    private void Attack(DamageInfo attack)
    {
        //aici avem un trigger pentru animaþii (poþi sã mai adaugi ºi tu pentru arc ºi magie)
        //animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange);

        foreach (Collider2D enemy in hitEnemies)
        {
            if(enemy.gameObject == this.gameObject)
            {
                continue;
            }

            IDamageable damageable = enemy.GetComponent<IDamageable>();
            Debug.Log("Hit on Enemy");
            if (damageable != null)
            {
                damageable.TakeDamage(attack);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
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

    public void TakeDamage(DamageInfo damage)
    {
        health = Mathf.Clamp(health - damage.Amount, 0, health);

        if (health == 0)
        {
            Destroy(gameObject);
        }
        healthBar.fillAmount = Mathf.Clamp(Health / MaximumHealth, 0, 1);

        if (damage.Intterupts && isIntterupteble)
        {
            ApplyEffect(damage.Effect);
        }
    }

    private void ApplyEffect(Response effect)
    {
        //Logica pentru aplicarea efectului specific, ex: `Stagger`, `Burn` etc.
    }
}
