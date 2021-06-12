using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phaser : PowerUp
{
    [SerializeField]
    protected LayerMask phasingLayer;
    [SerializeField]
    protected float phasingAlpha = 100;
    [SerializeField]
    protected LayerMask normalLayer;
    [SerializeField]
    protected float normalAlpha = 255;

    protected virtual void Awake()
    {
        powerUpType = PowerUps.Phaser;
        possibleCombos.Add(PowerUps.Jetpack, PowerUps.JetPhaser);
        possibleCombos.Add(PowerUps.Blaster, PowerUps.PhaseBlaster);
    }

    public override void OnPowerStop()
    {
        powerUpHander.gameObject.layer = normalLayer;
        Color newColor = powerUpHander.GetComponent<SpriteRenderer>().color;
        newColor.a = normalAlpha;
        powerUpHander.GetComponent<SpriteRenderer>().color = newColor;
    }

    public override void OnPowerUse()
    {
        powerUpHander.gameObject.layer = phasingLayer;
        Color newColor = powerUpHander.GetComponent<SpriteRenderer>().color;
        newColor.a = phasingLayer;
        powerUpHander.GetComponent<SpriteRenderer>().color = newColor;
    }
}
