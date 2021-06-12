using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseBlasterBullet : BlasterBullets
{
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        // Set up the phase

        base.OnCollisionEnter2D(collision);
    }
}
