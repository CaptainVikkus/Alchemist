using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
       CheckpointHandler handler = collision.gameObject.GetComponent<CheckpointHandler>();

        if (handler)
        {
            handler.SetCheckpoint(transform);
        }
    }
}
