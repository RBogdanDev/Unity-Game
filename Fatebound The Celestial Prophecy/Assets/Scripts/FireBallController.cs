using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Scripts{
public class FireBallController : MonoBehaviour
{
    public float speed=50f;
    private AudioSource audioSource;
    public AudioClip impactClip, forceClip;
    private Rigidbody2D rb;
    private BoxCollider2D box;
    private SpriteRenderer sr;
    private Animator animator;
    public bool isFacingRight=true;//by default personajul se misca spre dreapta
    public void Start()
    {
        //extragem toate componentele esentiale din animator
        animator=GetComponent<Animator>();
        box=GetComponent<BoxCollider2D>();
        rb=GetComponent<Rigidbody2D>();
        sr=GetComponent<SpriteRenderer>();
        audioSource=GetComponent<AudioSource>();
        audioSource.PlayOneShot(impactClip);
        //la start, activam sprite renderer pentru a afisa imaginea fireball-ului si boxcollider pentru campul de forta
        sr.enabled = true;
        box.enabled = true;

        //dam play la animatie
        animator.Play("Fireball_animation");
        Debug.Log("Fireball started!");
    }

    // Update is called once per frame
   public void Update()
    {
         if(isFacingRight==true)
         {
            transform.rotation = Quaternion.Euler(0, 0, 0); // rotim spre dreapta
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
         else{
            transform.rotation = Quaternion.Euler(0, 180, 0); //rotim spre stanga
            rb.velocity = new Vector2(-speed, rb.velocity.y);
         }
    }
    
    public void OnCollisionEnter2D(Collision2D collision)
    {

        StartCoroutine(PlayForceClip());//sunetul pentru impactul dintre fireball si orice obiect solid
        if (collision.gameObject.CompareTag("Enemy"))//cand se intampla coliziunea intre fireball si enemy se distruge!
        {
           Animator enemyAnimator = collision.gameObject.GetComponent<Animator>();//pornim animatia de dizzy
                if (enemyAnimator != null)
                {
                    enemyAnimator.Play("Specialhurt_spike");
                    StartCoroutine(ReturnToIdleAfterDelay(1.0f));
                }
           IDamageable damageableEnemy = collision.gameObject.GetComponent<IDamageable>();//aplicam efectiv damage
                if (damageableEnemy != null)
                {
          
            DamageInfo fireballDamage = new DamageInfo(
                30,
                Type.Melee,
                Response.Stagger,
                0.5f,
                0f,
                false
            );

            damageableEnemy.TakeDamage(fireballDamage, transform.position);
                }
           Debug.Log("Target hit!");
           Destroy(gameObject);
        }
        else if(collision.gameObject.CompareTag("Grid"))
        {
            Debug.Log("Grid hit!");
            Destroy(gameObject);
        }
    }
    IEnumerator ReturnToIdleAfterDelay(float delay)
{
    yield return new WaitForSeconds(delay);
    animator.Play("idle");
}
private IEnumerator PlayForceClip()
{
    if (forceClip != null)
    {
        audioSource.PlayOneShot(forceClip);
        yield return new WaitForSeconds(forceClip.length);
    }
    else
    {
        Debug.LogError("ForceClip is not assigned!");
        yield return null;
    }
}
}
}
