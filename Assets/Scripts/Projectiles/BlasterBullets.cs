using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterBullets : MonoBehaviour
{
    [SerializeField]
    protected float speed;
    protected Vector2 direction = new Vector2(0,0);
    protected Rigidbody2D rigid;

    // Update is called once per frame
    void Update()
    {
        rigid.AddForce(direction * speed, ForceMode2D.Impulse);
    }

    public void setDirection(Vector2 newDirection)
    {
        direction = newDirection;
    }


}
