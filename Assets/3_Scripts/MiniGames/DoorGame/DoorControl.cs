using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace MiniGame.DoorGame
{
    public class DoorControl : MonoBehaviour, IInteractable
    {
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
        }

        private void CheckDoorStatus()
        {
            if(hasWoodCount > 0) return;
            _collider.enabled = true;
            CanInteractable = true;
        }

        private void OpenDoor()
        {
            _collider.enabled = false;
            door.DORotate(new Vector3(0, -90, 0), 1);
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
