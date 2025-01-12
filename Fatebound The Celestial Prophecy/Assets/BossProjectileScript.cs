using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Scripts{
public class BossProjectileScript : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb;
    private BoxCollider2D box;
    private SpriteRenderer sr;
    private Animator animator;
    private AudioSource audioSource;
    public AudioClip impactClip, forceClip;
    public float force;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        float rot = Mathf.Atan2(-direction.x, -direction.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
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
        Debug.Log("Enemy_Fireball_started!");
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {

        StartCoroutine(PlayForceClip());//sunetul pentru impactul dintre fireball si orice obiect solid
        if (collision.gameObject.CompareTag("Player"))//cand se intampla coliziunea intre fireball si enemy se distruge!
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
    private IEnumerator PlayForceClip(){
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
    IEnumerator ReturnToIdleAfterDelay(float delay){
        yield return new WaitForSeconds(delay);
        animator.Play("idle");
    }
}
}


