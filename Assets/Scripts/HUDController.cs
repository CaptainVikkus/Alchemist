using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public GameObject powerupUI;
    public Image powerupImage;


    public void OnPowerUpReceive(int type)
    {
        powerupUI.SetActive(true);

        switch (type)
        {
            case 0:
                powerupImage.color = Color.magenta;
                break;
            case 1:
                powerupImage.color = Color.cyan;
                break;
            case 2:
                powerupImage.color = Color.yellow;
                break;
            default:
                break;
        }
    }

    public void OnPowerUpLost()
    {
        powerupUI.SetActive(false);
    }
}
