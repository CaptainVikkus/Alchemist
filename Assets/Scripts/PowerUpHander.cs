using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpHander : MonoBehaviour
{
    [SerializeField]
    private GameObject powerupHolder;
    private List<PowerUp> powerUps = new List<PowerUp>();
    private PowerUp currentPowerUp;

    public void Start()
    {
        powerUps.AddRange(powerupHolder.GetComponents<PowerUp>());
    }
    public void SwitchPowerUp(PowerUps newPowerUp)
    {
        if (currentPowerUp != null)
        {
            currentPowerUp.OnPowerStop();
            PowerUps powerUpCombo = PowerUps.Num_Of_Powerups;
            currentPowerUp.PossibleCombos.TryGetValue(newPowerUp, out powerUpCombo);
            if (powerUpCombo != PowerUps.Num_Of_Powerups)
            { 
                newPowerUp = powerUpCombo;
            }
        }
        
        currentPowerUp = powerUps[(int)newPowerUp];
        currentPowerUp.LinkPlayerHandler(this);
    }

    public void UsePowerUp()
    {
        if (currentPowerUp != null)
        {
            currentPowerUp.OnPowerUse();
        }
    }

    public void StopPowerUP()
    {
        if (currentPowerUp != null)
        {
            currentPowerUp.OnPowerStop();
        }
    }
}
