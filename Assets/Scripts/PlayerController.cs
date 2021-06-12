using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    public float sensitivity = 0.1f;
    public float speed = 50f;
    public float jumpForce = 500f;

    private Animator playerAnimator;
    private SpriteRenderer playerSprite;
    private Rigidbody2D playerRb2d;
    private PowerUpHander powerUpHander;
    private Vector2 movement;

    public readonly int IsJumpingHash = Animator.StringToHash("IsJumping");
    public readonly int IsWalkingHash = Animator.StringToHash("IsWalking");
    public readonly int IsFiringHash = Animator.StringToHash("IsFiring");

    public bool IsJumping { get; private set; }
    public bool IsFlipped { get; private set; }
    public bool IsWalking { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();
        playerRb2d = GetComponent<Rigidbody2D>();
        powerUpHander = GetComponent<PowerUpHander>();
    }

    private void Update()
    {
        if (!IsJumping) //Only move if not jumping
        {
            if (IsWalking)
            {
                playerRb2d.velocity = new Vector2(
                    (IsFlipped ? -1f: 1f) * speed,
                    playerRb2d.velocity.y);
            }
            else
            {
                playerRb2d.velocity = new Vector2(
                    0f,
                    playerRb2d.velocity.y); 
            }
        }
    }

    public void OnMove(InputValue input)
    {
        movement = input.Get<Vector2>();
        if (movement.x > sensitivity)
        {
            IsWalking = true;
            IsFlipped = false;
        }
        else if (movement.x < -sensitivity)
        {
            IsWalking = true;
            IsFlipped = true;
        }
        else
        {
            IsWalking = false;
        }

        playerAnimator.SetBool(IsWalkingHash, IsWalking);
        playerSprite.flipX = IsFlipped;
    }

    public void OnJump(InputValue input)
    {
        if (IsJumping) return; //no double jump

        playerAnimator.SetBool(IsJumpingHash, true);
        IsJumping = true;

        playerRb2d.AddForce(Vector2.up * jumpForce);
    }


    public void OnPower(InputValue input)
    {
        playerAnimator.SetBool(IsFiringHash, input.isPressed);

        if (input.isPressed)
        { //Power Use
           
        }
        else 
        { //Power Stop 
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            IsJumping = false;
            playerAnimator.SetBool(IsJumpingHash, false);
        }
    }
}
