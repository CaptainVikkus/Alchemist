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
    public AudioClip walkSound;
    public AudioClip jumpSound;
    public AudioClip landSound;


    private Animator playerAnimator;
    private AudioSource playerAudio;
    private SpriteRenderer playerSprite;
    private Rigidbody2D playerRb2d;
    private PowerUpHander powerUpHander;
    private Vector2 movement;
    [SerializeField]
    Transform mainfirePoint;
    private bool usingPower;

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
        playerAudio = GetComponent<AudioSource>();
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

        if (usingPower)
        {
            powerUpHander.UsePowerUp();
        }
    }

    public void SetIsJumping(bool isJumping)
    {
        this.IsJumping = isJumping;
    }
    public void OnMove(InputValue input)
    {
        movement = input.Get<Vector2>();
        if (movement.x > sensitivity)
        {
            IsWalking = true;
            IsFlipped = false;
            mainfirePoint.localPosition = new Vector3(1.25f, 0f, 0);
        }
        else if (movement.x < -sensitivity)
        {
            IsWalking = true;
            IsFlipped = true;
            mainfirePoint.localPosition = new Vector3(-1.25f, 0f, 0);
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

        playerAudio.PlayOneShot(jumpSound);

        playerAnimator.SetBool(IsJumpingHash, true);
        IsJumping = true;

        playerRb2d.AddForce(Vector2.up * jumpForce);
    }


    public void OnPower(InputValue input)
    {

        if (input.isPressed)
        { //Power Use
            powerUpHander.UsePowerUp();
            usingPower = true;
        }
        else 
        { //Power Stop 
            powerUpHander.StopPowerUP();
            usingPower = false;
        }
    }

    public void PlayFootstep()
    {
        playerAudio.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
        playerAudio.PlayOneShot(walkSound);
        playerAudio.pitch = 1f;
    }

    public void PlayFiringAnim(bool play)
    {
        playerAnimator.SetBool(IsFiringHash, play);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {        
            if (IsJumping) { playerAudio.PlayOneShot(landSound); }

            IsJumping = false;
            playerAnimator.SetBool(IsJumpingHash, false);

        }
    }
}
