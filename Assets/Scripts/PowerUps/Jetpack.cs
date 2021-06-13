using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jetpack : PowerUp
{
    [SerializeField]
    protected float thrustForce = 0.5f;
    protected Rigidbody2D rigid;
    [SerializeField]
    protected AudioClip jetpackSoundEffect;

    public override void OnPowerStop()
    {
        powerUpHander.Player.playerAudio.Stop();
    }

    public override void OnPowerUse()
    {
        powerUpHander.Player.SetIsFlying(true);
        rigid.AddForce(rigid.transform.up * thrustForce * Time.deltaTime, ForceMode2D.Impulse);

        // Sound effect
        if (!powerUpHander.Player.playerAudio.isPlaying)
        { powerUpHander.Player.playerAudio.loop = false;
            powerUpHander.Player.playerAudio.clip = jetpackSoundEffect;
            powerUpHander.Player.playerAudio.Play();
        }
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
