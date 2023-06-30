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
        private void Update()
        {
            Debug.DrawRay(muzzleSocket.position, muzzleSocket.forward * 20f, Color.red);
        }

        public void Shoot()
        {
            //Determine the rotation that we want to shoot our projectile in.
            Quaternion rotation = Quaternion.LookRotation(transform.forward * 1000.0f - muzzleSocket.position);
            
            //If there's something blocking, then we can aim directly at that thing, which will result in more accurate shooting.
            if (Physics.Raycast
                (
                    new Ray(muzzleSocket.position, muzzleSocket.forward), 
                    out RaycastHit hit, 
                    1000,
                    targetLayer))
            {
                rotation = Quaternion.LookRotation(hit.point - muzzleSocket.position);
            }
            GameObject projectile = Instantiate(prefabProjectile, muzzleSocket.position, rotation);
            projectile.GetComponent<Rigidbody>().velocity = projectile.transform.forward * projectileImpulse;
        }
    }
}
