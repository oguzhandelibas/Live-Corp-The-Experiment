using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace MiniGame.RaidGame
{
    public class RaidTarget : MonoBehaviour
    {
        [SerializeField] private RaidControl _raidControl;
    
        public void TakeHit()
        {
            StartCoroutine(HitRoutine());
        }

        IEnumerator HitRoutine()
        {
            transform.DOLocalRotate(new Vector3(0, 0, -90), 1);
            yield return new WaitForSeconds(1);
            _raidControl.FinishSlowMotionGame();
            
        }
    }
}
