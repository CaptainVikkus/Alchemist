using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPhaser : Jetpack
{
    [SerializeField]
    protected LayerMask phasingLayer;
    [SerializeField]
    protected float phasingAlpha = 100;
    [SerializeField]
    protected LayerMask normalLayer;
    [SerializeField]
    protected float normalAlpha = 255;

    protected override void Awake()
    {
        powerUpType = PowerUps.JetPhaser;
    }

    public override void OnPowerStop()
    {
        base.OnPowerStop();
        powerUpHander.gameObject.layer = normalLayer;
        Color newColor = powerUpHander.GetComponent<SpriteRenderer>().color;
        newColor.a = normalAlpha;
        powerUpHander.GetComponent<SpriteRenderer>().color = newColor;
    }

    public override void OnPowerUse()
    {
        base.OnPowerUse();
        powerUpHander.gameObject.layer = phasingLayer;
        Color newColor = powerUpHander.GetComponent<SpriteRenderer>().color;
        newColor.a = phasingLayer;
        powerUpHander.GetComponent<SpriteRenderer>().color = newColor;
    }
}
