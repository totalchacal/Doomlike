using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHealthPowerUp : PowerUp
{
    public float amount = 10f;

    protected override void PowerUpPayload()  // Checklist item 1
    {
        base.PowerUpPayload();

        playerController.AddHealth(amount);      
    }
}
