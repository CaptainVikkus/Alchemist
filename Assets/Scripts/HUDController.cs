using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public GameObject powerupUI;
    public Image powerupImage;


    public void OnPowerUpReceive(PowerUps type)
    {
        powerupUI.SetActive(true);

        switch (type)
        {
            case PowerUps.Jetpack:
                powerupImage.color = Color.cyan;
                break;
            case PowerUps.Blaster:
                powerupImage.color = Color.yellow;
                break;
            case PowerUps.Phaser:
                powerupImage.color = Color.magenta;
                break;
            case PowerUps.JetBlaster:
                powerupImage.color = Color.green;
                break;
            case PowerUps.JetPhaser:
                powerupImage.color = Color.blue;
                break;
            case PowerUps.PhaseBlaster:
                powerupImage.color = Color.red;
                break;
            default:
                OnPowerUpLost();
                break;
        }
    }

    public void OnPowerUpLost()
    {
        powerupUI.SetActive(false);
    }
}
