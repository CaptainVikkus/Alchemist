using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabBehaviour : MonoBehaviour
{
    public float speed = 1f;
    public float behaviourSpeed = 1f;
    public Transform groundCheck;
    public AudioClip deathSound;
    public AudioClip movementSound;
    public Animator deathAnimatior;

    private AudioSource audio;

    private float timeElapsed = 0f;
    private bool IsWalking = false;
    private Animator animator;
    public readonly int IsWalkingHash = Animator.StringToHash("IsWalking");
    private Collider2D collider2;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidbody2D;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        collider2 = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        //Execute Behaviour
        if (timeElapsed < behaviourSpeed)
        {
            if (IsWalking)
            {
                Walk();
            }

            timeElapsed += Time.deltaTime;
        }
        //New Behaviour
        else
        {
            IsWalking = !IsWalking;
            //50/50 to flip
            if (Random.Range(0f, 2f) > 1f)
            {
                transform.right *= -1;
            }

            animator.SetBool(IsWalkingHash, IsWalking);
            timeElapsed = 0f;
        }
    }

    private void Walk()
    {
        //Ground Check
        if (!Physics2D.OverlapCircle(groundCheck.position, 0.1f, LayerMask.GetMask("Ground")))
        { //Flip away from edge
            transform.right *= -1;
        }
        //Move Forwards
        transform.position += transform.right * speed * Time.deltaTime;
    }

    public void PlayFootstep()
    {
        audio.PlayOneShot(movementSound);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("Hit Player");
            collision.gameObject.GetComponent<CheckpointHandler>().TeleportToCheckpoint();
        }
        else if (collision.gameObject.GetComponent<PhaseBlasterBullet>() != null)
        {
            gameObject.layer = LayerMask.NameToLayer("Phasing");
            rigidbody2D.gravityScale = 0;
            rigidbody2D.velocity = new Vector2();
            collider2.isTrigger = true;
            Color newColor = spriteRenderer.color;
            newColor.a = 0.3f;
            spriteRenderer.color = newColor;
        }
        else if (collision.gameObject.GetComponent<BlasterBullets>() != null)
        {
            collider2.isTrigger = true;
            rigidbody2D.gravityScale = 0;
            spriteRenderer.enabled = false;
            audio.clip = deathSound;
            audio.loop = false;
            audio.Play();
            StartCoroutine(PlayDeadEffect());
        }
    }

    IEnumerator PlayDeadEffect()
    {
        deathAnimatior.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        deathAnimatior.enabled = true;
        deathAnimatior.Play("EnemyDeathAnim");
        yield return new WaitForSeconds(deathAnimatior.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }
}
