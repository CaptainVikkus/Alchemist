using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctopsBehaviour : MonoBehaviour
{
    public float speed = 1.0f;
    public float pathTolerance = 0.1f;
    public Vector3 patrol1 = Vector3.left;
    public Vector3 patrol2 = Vector3.right;

    private Vector3 worldPatrol1;
    private Vector3 worldPatrol2;
    private Vector3 target;
    private bool patrolpicker;

    // Start is called before the first frame update
    void Start()
    {
        worldPatrol1 = transform.position + patrol1;
        worldPatrol2 = transform.position + patrol2;
        target = worldPatrol1;
        patrolpicker = true;
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
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + patrol1, 0.1f);
        Gizmos.DrawSphere(transform.position + patrol2, 0.1f);
    }
}
