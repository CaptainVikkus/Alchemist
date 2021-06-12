using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
       CheckpointHandler handler = collision.gameObject.GetComponent<CheckpointHandler>();

        if (handler)
        {
            handler.SetCheckpoint(transform);
        }
    }
}
