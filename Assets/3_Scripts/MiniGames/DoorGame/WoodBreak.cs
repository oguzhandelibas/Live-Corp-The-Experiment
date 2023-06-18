using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MiniGame.DoorGame
{
    public class WoodBreak : MonoBehaviour
    {
        [SerializeField] private Rigidbody[] woodPiecesRigidbodies;
        private BoxCollider _collider;

        private void Start()
        {
            _collider = GetComponent<BoxCollider>();
        }

        public void Break()
        {
            _collider.enabled = false;
            foreach (Rigidbody item in woodPiecesRigidbodies)
            {
                item.isKinematic = false;
                item.AddForce(new Vector3(Random.Range(10,20),Random.Range(10,20),Random.Range(10,20)), ForceMode.Impulse);
            }

            GetComponentInParent<DoorControl>().HasWoodCount--;
        }
    }
}

