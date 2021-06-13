using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumboBehaviour : MonoBehaviour
{
    private Collider2D collider2;
    private SpriteRenderer spriteRenderer;
    private void Start()
    {
        collider2 = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PhaseBlasterBullet>() != null)
        {
            gameObject.layer = LayerMask.NameToLayer("Phasing");
            collider2.isTrigger = true;
            Color newColor = spriteRenderer.color;
            newColor.a = 0.3f;
            spriteRenderer.color = newColor;
        }
    }
}
