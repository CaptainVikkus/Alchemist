using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointHandler : MonoBehaviour
{
    public Transform checkpointSpawn;

    public void SetCheckpoint(Transform checkpoint)
    {
        checkpointSpawn = checkpoint;
    }

    public void TeleportToCheckpoint()
    {
        transform.position = checkpointSpawn.position;
    }
}
