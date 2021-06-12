using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehaviour : MonoBehaviour
{
    public float oscilationStrength = .5f;
    public float rotationStrength = 5.0f;
    public int type;
    public GameObject background;
    public SpriteRenderer backgroundSprite;

    private readonly int PowerUpTypeHash = Animator.StringToHash("PowerUpType");

    private void Start()
    {
        switch (type)
        {
            case 0:
                backgroundSprite.color = Color.magenta;
                break;
            case 1:
                backgroundSprite.color = Color.cyan;
                break;
            case 2:
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
}
