using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : PowerUp
{
    public float amount = 0.5f;

    protected override void PowerUpPayload()  // Checklist item 1
    {
        base.PowerUpPayload();

        playerController.walkingSpeed += amount;
        playerController.runningSpeed += amount;
    }
}
