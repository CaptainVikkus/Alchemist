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
    protected bool impacted;



    // Update is called once per frame
    protected virtual void Update()
    {
        rigid.AddForce(direction * speed, ForceMode2D.Impulse);
    }

    public void setDirection(Vector2 newDirection)
    {
        direction = newDirection;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (!impacted)
        {
            impacted = true;
            StartCoroutine(PlayImpactAnimation());
        }
    }

    protected virtual IEnumerator PlayImpactAnimation()
    {
        animator.enabled = true;
        animator.Play("ImpactAnim");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(1).length);
        Destroy(gameObject);
    }
}
