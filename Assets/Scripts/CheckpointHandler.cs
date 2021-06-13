using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointHandler : MonoBehaviour
{
    public Transform checkpointSpawn;
    [SerializeField] private AudioClip respawnSound;
    [SerializeField] private AudioClip checkpointSound;

    public void SetCheckpoint(Transform checkpoint)
    {
        if (checkpoint.position == checkpointSpawn.position) { return; }

        //New Checkpoint
        checkpointSpawn = checkpoint;
        GetComponent<AudioSource>().PlayOneShot(checkpointSound);
    }

    public void TeleportToCheckpoint()
    {
        transform.position = checkpointSpawn.position;
        GetComponent<AudioSource>().PlayOneShot(respawnSound);
    }
}
