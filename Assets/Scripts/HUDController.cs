using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class HUDController : MonoBehaviour
{
    public GameObject pauseUI;
    public GameObject powerupUI;
    public Image powerupImage;

    private bool IsPaused = false;

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

    public void OnPause(InputValue input)
    {
        TogglePause();
    }

    public void TogglePause()
    {
        IsPaused = !IsPaused;
        GetComponent<PlayerController>().enabled = IsPaused;
        pauseUI.SetActive(IsPaused);
        Time.timeScale = IsPaused ? 1f : 0f;
    }
}
