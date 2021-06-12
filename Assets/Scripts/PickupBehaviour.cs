using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehaviour : MonoBehaviour
{
    public float oscilationStrength = .5f;
    public float rotationStrength = 5.0f;
    public PowerUps type;
    public GameObject background;
    public SpriteRenderer backgroundSprite;

    private readonly int PowerUpTypeHash = Animator.StringToHash("PowerUpType");

    private void Start()
    {
        switch (type)
        {
            case PowerUps.Phaser:
                backgroundSprite.color = Color.magenta;
                break;
            case PowerUps.Jetpack:
                backgroundSprite.color = Color.cyan;
                break;
            case PowerUps.Blaster:
                backgroundSprite.color = Color.yellow;
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * Mathf.Sin(Time.time) * (oscilationStrength/1000f);
        background.transform.Rotate(Vector3.forward * rotationStrength * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PowerUpHander hander = collision.gameObject.GetComponent<PowerUpHander>();

        if (hander != null)
        {
            hander.SwitchPowerUp(type);
            Destroy(gameObject);
        }
    }
}
