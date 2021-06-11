using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    public float sensitivity = 0.1f;
    private Animator playerAnimator;

    public readonly int IsJumpingHash = Animator.StringToHash("IsJumping");
    public readonly int IsWalkingHash = Animator.StringToHash("IsWalking");

    public bool IsJumping { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    public void OnMove(InputValue input)
    {
        Vector2 movement = input.Get<Vector2>();
        playerAnimator.SetBool(IsWalkingHash, movement.magnitude > sensitivity);
    }

    public void OnJump(InputValue input)
    {
        if (IsJumping) return; //no double jump

        playerAnimator.SetBool(IsJumpingHash, true);
        IsJumping = true;

        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        playerAnimator.SetBool(IsJumpingHash, false);
        IsJumping = false;
        Debug.Log("Land");
    }
}
