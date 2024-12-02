using System.Collections;
using UnityEngine;
namespace Scripts{
public class NinjaController : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip attackSound, walkSound;
    private Animator animator;

    [SerializeField] public GameObject fireBallPrefab;
    public Transform spawnPoint;

    void Start()
    {
        animator = GetComponent<Animator>();
        fireBallPrefab.SetActive(false);
        animator.Play("Idle_animation"); // pornim Idle
        audioSource=GetComponent<AudioSource>();
    }

    void Update()//apelam handlerele la fiecare frame
    {
        HandleMovement();
        HandleInput();
    }

    //handler de gestionare a movement ului
    private void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal"); // AD
        float moveY = Input.GetAxis("Vertical");   // WS

        animator.SetFloat("moveX", moveX);
        animator.SetFloat("moveY", moveY);

        if (moveX < 0)
        {
            transform.localScale = new Vector3(-0.45f, 0.48f, 1); // rotim personajul la stanga
        }
        else if (moveX > 0)
        {
            transform.localScale = new Vector3(0.45f, 0.48f, 1); //rotim personajul la dreapta
        }

        bool isRunning = moveX != 0 || moveY != 0;
        animator.SetBool("isRunning", isRunning); // activam animatia de running
    }

    // handler de user input
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleCrouch();
        }

        if (Input.GetMouseButtonDown(0)) // click stanga pentru atac
        {
            animator.SetTrigger("attack");
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            animator.SetTrigger("isSpecial");
        }
    }
    public void SpawnFireball()
    { 
        GameObject newFireball = Instantiate(fireBallPrefab, transform.position, Quaternion.identity);
        newFireball.SetActive(true); // Activăm noua instanță dacă este necesar (ar trebui să fie activată implicit)
        FireBallController controller = newFireball.GetComponent<FireBallController>();
        if (controller != null)///activam script ul dedicat fireball ului
           {
                  controller.enabled = true; 
                  controller.isFacingRight= transform.localScale.x > 0;//un indicator al orientarii personajului ninja
                  controller.Start();//aici dam drumul explicit la functia start
           } 
        Debug.Log("Fireball spawned!");
    }


    // Toggle pentru crouch
    private void ToggleCrouch()
    {
        bool isCrouch = animator.GetBool("isCrouch");
        animator.SetBool("isCrouch", !isCrouch);
    }
   
   public void PlayAttackSound()
    {
        audioSource.PlayOneShot(attackSound);
    }
    public void PlayWalkSound()
    {
        audioSource.PlayOneShot(walkSound); // Redă sunetul asociat
    }

}
}

