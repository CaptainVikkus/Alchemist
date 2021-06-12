using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabBehaviour : MonoBehaviour
{
    public float speed = 1f;
    public float behaviourSpeed = 1f;
    public Vector3 groundCheck;

    private float timeElapsed = 0f;
    private bool IsWalking = false;
    private Animator animator;
    public readonly int IsWalkingHash = Animator.StringToHash("IsWalking");

    private void Start()
    {
        animator = GetComponent<Animator>();
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
        Vector2 point = transform.position + groundCheck;
        if (!Physics2D.OverlapCircle(point, 0.1f, LayerMask.GetMask("Ground")))
        { //Flip away from edge
            transform.right *= -1;
        }
        //Move Forwards
        transform.position += transform.right * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("Hit Player");
            collision.gameObject.GetComponent<CheckpointHandler>().TeleportToCheckpoint();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + groundCheck, 0.1f);
    }
}
