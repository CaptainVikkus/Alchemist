using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerousTerrain : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("Hit Player");
            collision.gameObject.GetComponent<CheckpointHandler>().TeleportToCheckpoint();
        }
    }
}
