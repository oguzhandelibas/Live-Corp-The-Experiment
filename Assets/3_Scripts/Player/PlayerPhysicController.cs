using System;
using System.Collections;
using System.Collections.Generic;
using MiniGame.YesYes;
using UnityEngine;

public class PlayerPhysicController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out YesYesController yesController))
        {
            yesController.ActivateChoosePlatform();
        }
    }
}
