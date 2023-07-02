using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPC
{
    public class NPCShootControl : MonoBehaviour
    {
        [SerializeField] private Transform muzzleSocket;
        [SerializeField] private GameObject prefabProjectile;
        [SerializeField] private float projectileImpulse;
        [SerializeField] private LayerMask targetLayer;
        [SerializeField] private  Transform player;
        private Vector3 direction;

        private void Start()
        {
            
        }

        private void Update()
        {
            direction = player.position - muzzleSocket.position;
            direction.Normalize();
            Debug.DrawRay(muzzleSocket.position, direction * 10.0f, Color.red);
        }

        public void Shoot()
        {
            //Determine the rotation that we want to shoot our projectile in.
            Quaternion rotation = Quaternion.LookRotation(transform.forward - muzzleSocket.position);
            //If there's something blocking, then we can aim directly at that thing, which will result in more accurate shooting.
            if (Physics.Raycast
                (
                    new Ray(muzzleSocket.position, direction), 
                    out RaycastHit hit, 
                    10.0f,
                    targetLayer))
            {
                rotation = Quaternion.LookRotation(direction);
            }
            GameObject projectile = Instantiate(prefabProjectile, muzzleSocket.position, rotation);
            projectile.GetComponent<Rigidbody>().velocity = direction * projectileImpulse;
        }
    }
}
