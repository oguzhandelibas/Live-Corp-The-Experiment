using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace MiniGame.DoorGame
{
    public class DoorControl : MonoBehaviour, IInteractable
    {
        public UnityEvent OnDoorOpen;
        [SerializeField] private Animator doorAnimator;
        [SerializeField] private Transform door;
        [SerializeField] private BoxCollider _collider;
        private int hasWoodCount;
        
        public int HasWoodCount
        {
            get => hasWoodCount;
            set
            {
                hasWoodCount = value; 
                CheckDoorStatus();
                
            }
        }
        public bool CanInteractable { get; set; }

        private void Start()
        {
            hasWoodCount = GetComponentsInChildren<WoodBreak>().Length;
            doorAnimator.SetBool("character_nearby", false);
        }

        private void CheckDoorStatus()
        {
            if(hasWoodCount > 0) return;
            _collider.enabled = true;
            CanInteractable = true;
        }

        private void OpenDoor()
        {
            OnDoorOpen?.Invoke();
            doorAnimator.SetBool("character_nearby", true);
            _collider.enabled = false;
            //door.DOLocalRotate(new Vector3(0, -90, 0), 1);
        }

        public void InteractStart(GameObject interactObject, Transform parent)
        {
            if (CanInteractable) OpenDoor();
        }

        public void OnInteract()
        {
        }

        public void InteractEnd()
        {
        }
    }
}
