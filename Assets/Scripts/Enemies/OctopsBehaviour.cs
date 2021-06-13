using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctopsBehaviour : MonoBehaviour
{
    public float speed = 1.0f;
    public float pathTolerance = 0.1f;
    public Vector3 patrol1 = Vector3.left;
    public Vector3 patrol2 = Vector3.right;
    public AudioClip deathSound;
    public AudioClip movementSound;
    public Animator deathAnimatior;

    private AudioSource audio;
    private Vector3 worldPatrol1;
    private Vector3 worldPatrol2;
    private Vector3 target;
    private bool patrolpicker;
    private Collider2D collider2;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        worldPatrol1 = transform.position + patrol1;
        worldPatrol2 = transform.position + patrol2;
        target = worldPatrol1;
        patrolpicker = true;

        audio = GetComponent<AudioSource>();
        collider2 = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(PlayWalkDelayed());
    }

    // Update is called once per frame
    void Update()
    {
        //Patrol
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        //Switch Patrol
        if (Vector3.Distance(transform.position, target) <= pathTolerance)
        {
            patrolpicker = !patrolpicker;
            target = patrolpicker ? worldPatrol1 : worldPatrol2;
        }
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
            collider2.isTrigger = true;
        }
        else if (collision.gameObject.GetComponent<BlasterBullets>() != null)
        {
            collider2.isTrigger = true;
            spriteRenderer.enabled = false;
            audio.clip = deathSound;
            audio.loop = false;
            audio.Play();
            StartCoroutine(PlayDeadEffect());
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + patrol1, 0.1f);
        Gizmos.DrawSphere(transform.position + patrol2, 0.1f);
    }

    IEnumerator PlayWalkDelayed()
    {
        while (isActiveAndEnabled)
        {
            audio.PlayOneShot(movementSound);
            yield return new WaitForSeconds(1.0f);
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
