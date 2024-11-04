using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    private float health, maximumHealth;

    public float Health => health;
    public float MaximumHealth => maximumHealth;

    private DamageInfo defaultAttack = new DamageInfo(15, Type.Melee, Response.Stagger, true);
    private bool isIntterupteble = true;

    public Transform AttackPoint;
    public float AttackRange = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        maximumHealth = 40;
        health = maximumHealth;

        StartCoroutine(CallFunctionAfterDelay());
    }

    // Update is called once per frame
    void Update() {}

    // Pentru a apela o functia de atac dupa un delay
    IEnumerator CallFunctionAfterDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            Attack(defaultAttack);
        }
    }

    private void Attack(DamageInfo attack)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.gameObject == this.gameObject)
            {
                continue;
            }

            IDamageable damageable = enemy.GetComponent<IDamageable>();
            Debug.Log("Hit on Player");
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

    public void TakeDamage(DamageInfo damage)
    {
        health = Mathf.Clamp(health - damage.Amount, 0, health);

        if (health == 0)
        {
            Destroy(gameObject);
        }

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
