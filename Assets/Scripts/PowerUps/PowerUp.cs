using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public enum PowerUps
{ 
    Jetpack,
    Blaster,
    Phaser,
    JetBlaster,
    JetPhaser,
    PhaseBlaster,
    Num_Of_Powerups,
}

public abstract class PowerUp : MonoBehaviour
{
    protected PowerUps powerUpType;
    public PowerUps PowerUpType => powerUpType;
    protected PowerUpHander powerUpHander;
    protected Dictionary<PowerUps, PowerUps> possibleCombos;
    public Dictionary<PowerUps, PowerUps> PossibleCombos => possibleCombos;

    public virtual void LinkPlayerHandler(PowerUpHander powerUpHander)
    {
        this.powerUpHander = powerUpHander;
    }

    public abstract void OnPowerUse();

    public abstract void OnPowerStop();

}
