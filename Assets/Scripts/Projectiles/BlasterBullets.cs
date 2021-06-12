using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterBullets : MonoBehaviour
{
    [SerializeField]
    protected float speed;
    protected Vector2 direction = new Vector2(0,0);
    [SerializeField]
    protected Rigidbody2D rigid;
    [SerializeField]
    protected Animator animator;
    [SerializeField]
    protected Collider2D collider;
    protected bool impacted;

    public void setDirection(Vector2 newDirection)
    {
        direction = newDirection;
        rigid.velocity = (direction * speed);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (!impacted)
        {
            impacted = true;
            rigid.velocity = new Vector2();
            collider.isTrigger = true;
            StartCoroutine(PlayImpactAnimation());
        }
    }

    protected virtual IEnumerator PlayImpactAnimation()
    {
        animator.enabled = true;
        animator.Play("ImpactAnim");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }
}
