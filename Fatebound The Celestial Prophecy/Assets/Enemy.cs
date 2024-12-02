using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Enemy : MonoBehaviour, IDamageable
{
    private Rigidbody2D rb;
    private AudioSource audioSource;
    public AudioClip attackClip, deadClip;
    private Animator animator;
    private float health, maximumHealth;

    public float Health => health;
    public float MaximumHealth => maximumHealth;

    private DamageInfo defaultAttack = new DamageInfo(10, Type.Melee, Response.Stagger, 0.2f, 0f, true);
    private bool isIntterupteble = true;

    public Transform AttackPoint;
    public float AttackRange = 5.0f;

    private bool IsStaggered = false;
    public EnemyAI enemyAI;
    private List<CancellationTokenSource> effectTokens = new List<CancellationTokenSource>();

    // Start is called before the first frame update
    void Start()
    {
        maximumHealth = 100;
        animator=GetComponent<Animator>();
        audioSource=GetComponent<AudioSource>();
        health = maximumHealth;

        rb = GetComponent<Rigidbody2D>();

        enemyAI = GetComponent<EnemyAI>();

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
            if (!IsStaggered)
            {
                Attack(defaultAttack);
            }
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

                animator.SetTrigger("jumpAttack");
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

    public void TakeDamage(DamageInfo damage, Vector2 attackVector)
    {
        var cts = new CancellationTokenSource();
        effectTokens.Add(cts);

        health = Mathf.Clamp(health - damage.Amount, 0, health);

        if (enemyAI != null)
        {
            Debug.Log("Calling ForceAggressive on EnemyAI from TakeDamage");
            enemyAI.ForceAggressive();
        }
        else
        {
            Debug.LogWarning("enemyAI reference is null in Enemy.cs!");
        }

        if (health <= 0)
        {
            animator.Play("Die_spike");
        }
        animator.SetTrigger("isHurt");
        // Verificam daca damage-ul primit de la inamic este unul care poate fi intrerupt
        if (damage.Intterupts && isIntterupteble)
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
                    }

                    yield return new WaitForSeconds(duration);
                    duration = 0;
                }
            }

            if (health <= 0)
            {
                animator.Play("Die_spike");
            }
            else if (IsStaggered)
            {
                IsStaggered = false;
            }
        }
    }
    public void DestroyAfterAnimation()
    {
        Destroy(gameObject);
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
    public void PlayAttackSoundEnemy()
    {
        audioSource.PlayOneShot(attackClip);
    }
    public void PlayDeadSound()
    {
        audioSource.PlayOneShot(deadClip);
    }
}
