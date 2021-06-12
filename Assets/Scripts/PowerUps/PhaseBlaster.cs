using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseBlaster : Blaster
{
    protected override void Awake()
    {
        powerUpType = PowerUps.PhaseBlaster;
    }
}
