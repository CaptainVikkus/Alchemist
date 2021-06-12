using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jetpack : PowerUp
{
    [SerializeField]
    protected float thrustForce = 0.5f;
    protected Rigidbody2D rigid;

    public override void OnPowerStop()
    {
        
    }

    public override void OnPowerUse()
    {
        rigid.AddForce(rigid.transform.up * thrustForce, ForceMode2D.Impulse);
    }

    protected virtual void Awake()
    {
        powerUpType = PowerUps.Jetpack;
        possibleCombos.Add(PowerUps.Blaster, PowerUps.JetBlaster);
        possibleCombos.Add(PowerUps.Phaser, PowerUps.JetPhaser);
    }

    public override void LinkPlayerHandler(PowerUpHander powerUpHander)
    {
        base.LinkPlayerHandler(powerUpHander);
        rigid = powerUpHander.GetComponent<Rigidbody2D>();
    }
}
