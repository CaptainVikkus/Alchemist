using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GoalBehaviour : MonoBehaviour
{
    public GameObject GameWinCanvas;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Do Win
            GameWinCanvas.SetActive(true);

            collision.gameObject.GetComponent<PlayerInput>().enabled = false;
            collision.gameObject.GetComponentInChildren<Camera>().transform.parent = null;

            collision.gameObject.GetComponent<PlayerController>().Yeet();

        }
    }
}
