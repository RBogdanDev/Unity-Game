using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using System;
using System.Threading;

public class Player : MonoBehaviour, IDamageable
{
    private float movespeed = 5;
    private AudioSource audioSource;
    public AudioClip deadClip;
    private Animator animator;
    private Rigidbody2D rb;
    private int Level, currentXP, maximumXP;
    private float health, maximumHealth;
    public UnityEngine.UI.Image healthBar;

    public float Health => health;
    public float MaximumHealth => maximumHealth;

    private DamageInfo[] attacks = new DamageInfo[]
    {
        new DamageInfo(10, Type.Melee, Response.KnockBack, 0.2f, 450f, true),
        new DamageInfo(1, Type.Melee, Response.Stun, 3f, 0f, true),
        new DamageInfo(50, Type.Melee, Response.Bleed, 5f, 1.5f, true)
    };
    private DamageInfo selectedAttack;
    private bool isInterruptible = true;

    public Transform AttackPoint;
    public float AttackRange = 5.0f;

    private bool IsStaggered = false;
    private List<CancellationTokenSource> effectTokens = new List<CancellationTokenSource>();

    void Start()
    {
        maximumHealth = 100;
        health = maximumHealth;
        rb = GetComponent<Rigidbody2D>();
        animator=GetComponent<Animator>();
        audioSource=GetComponent<AudioSource>();
        selectedAttack = attacks[0];
    }

    void Update()
    {
        if (IsStaggered)
        {
            return;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(horizontal, vertical) * movespeed;
        rb.velocity = movement;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack(selectedAttack);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedAttack = attacks[0];
            Debug.Log("Selected KnockBack");

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedAttack = attacks[1];
            Debug.Log("Selected Stun");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedAttack = attacks[2];
            Debug.Log("Selected Bleed");
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
                damageable.TakeDamage(attack, AttackPoint.transform.position);
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
    if (XPManager.Instance != null)
    {
        XPManager.Instance.onXPChange += HandleXPChange;
    }
    else
    {
        Debug.LogError("XPManager.Instance is null. Ensure it is initialized before enabling this script.");
    }
}

private void OnDisable()
{
    if (XPManager.Instance != null)
    {
        XPManager.Instance.onXPChange -= HandleXPChange;
    }
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

    public void TakeDamage(DamageInfo damage, Vector2 attackVector)
    {
        var cts = new CancellationTokenSource();
        effectTokens.Add(cts);

        health = Mathf.Clamp(health - damage.Amount, 0, health);

        if (health == 0)
        {
            animator.SetTrigger("isDead");
        }

        healthBar.fillAmount = Mathf.Clamp(Health / MaximumHealth, 0, 1);

        animator.SetTrigger("isHurt");//activam animatia de hurt
        // Verificam daca damage-ul primit de la inamic este unul care poate fi intrerupt
        if (damage.Interrupts || isInterruptible)
        {
            if (damage.Effect != Response.KnockBack)
            {
                // Daca nu este un KnockBack, aplicam efectul in paralel cu celelalte (daca exista)
                StartCoroutine(ApplyEffect(damage.Effect, damage.EffectDuration, damage.EffectDamage, cts.Token));
            }
            else
            {
                // Daca este un KnockBack, oprim toate efectele in desfasurare si aplicam KnockBack-ul
                StopAllEffects();
                IsStaggered = true;
                Vector2 knockBackDirection = ((Vector2)transform.position - attackVector).normalized;
                rb.AddForce(knockBackDirection * damage.EffectDamage, ForceMode2D.Impulse);
                StartCoroutine(StopKnockBack(knockBackDirection * damage.EffectDamage, damage.EffectDuration)); // Opreste KnockBack-ul dupa un timp
                Debug.Log("KnockBack");
            }
        }
    }
public void DestroyAfterAnimation()
{
    Destroy(gameObject);
}

    private IEnumerator ApplyEffect(Response effect, float duration, float damage, CancellationToken token)
    {
        if (effect != Response.None)
        {
            while (duration > 0)
            {
                if (health == 0 || token.IsCancellationRequested)
                    break;

                if (duration >= 1f)
                {
                    if (effect == Response.Stun)
                    {
                        if (!IsStaggered)
                        {
                            Debug.Log("Stuned");
                            IsStaggered = true;
                        }
                    }
                    else
                    {
                        Debug.Log(effect);
                        health = Mathf.Clamp(health - damage, 0, health);
                        healthBar.fillAmount = Mathf.Clamp(Health / MaximumHealth, 0, 1);
                    }

                    yield return new WaitForSeconds(1f);
                    duration -= 1f;
                }
                else
                {
                    if (effect == Response.Stagger || effect == Response.Stun)
                    {
                        if (!IsStaggered)
                        {
                            Debug.Log(effect);
                            IsStaggered = true;
                        }
                    }
                    else
                    {
                        Debug.Log(effect);
                        health = Mathf.Clamp(health - damage * (1 / duration), 0, health);
                        healthBar.fillAmount = Mathf.Clamp(Health / MaximumHealth, 0, 1);
                    }

                    yield return new WaitForSeconds(duration);
                    duration = 0;
                }
            }

            if (health == 0)
            {
                animator.SetTrigger("isDead");
            }
            else if (IsStaggered)
            {
                IsStaggered = false;
            }
        }
    }

    private void StopAllEffects()
    {
        foreach (var tokenSource in effectTokens)
        {
            tokenSource.Cancel(); // Cere oprirea fiecarui task specificat
            tokenSource.Dispose();
        }
        effectTokens.Clear(); // Curata lista dupa ce toate task-urile au fost anulate
    }

    private IEnumerator StopKnockBack(Vector2 force, float time)
    {
        yield return new WaitForSeconds(time); // Asteaptam timpul specificat
        rb.AddForce(-force, ForceMode2D.Impulse); // Aplicam o forta in directia opusa pentru a opri KnockBack-ul
        rb.velocity = Vector2.zero; // Oprim miscarea
        IsStaggered = false; // Oprim efectul de knockback
    }
    public void PlayDeadAnimation()
    {
        audioSource.PlayOneShot(deadClip);
    }
}