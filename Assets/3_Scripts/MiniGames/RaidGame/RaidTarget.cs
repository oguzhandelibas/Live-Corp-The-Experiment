using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace MiniGame.RaidGame
{
    public class RaidTarget : MonoBehaviour
    {
        public UnityEvent OnDie;

        public void TakeHit()
        {
            OnDie?.Invoke();                                                                                                                               
        }
    }
}
