using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpHander : MonoBehaviour
{
    [SerializeField]
    private GameObject powerupHolder;
    [SerializeField]
    private AudioClip powerPickupSound;
    [SerializeField]
    private AudioClip powerLostSound;

    private List<PowerUp> powerUps = new List<PowerUp>();
    private PowerUp currentPowerUp;
    private PlayerController player;
    public PlayerController Player => player;

    public void Start()
    {
        powerUps.AddRange(powerupHolder.GetComponents<PowerUp>());
        player = GetComponent<PlayerController>();
    }
    public void SwitchPowerUp(PowerUps newPowerUp)
    {
        if (currentPowerUp != null && currentPowerUp.PossibleCombos.ContainsKey(newPowerUp))
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

        Debug.Log(newPowerUp.ToString());

        player.hud.OnPowerUpReceive(newPowerUp);
        player.playerAudio.PlayOneShot(powerPickupSound);
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
