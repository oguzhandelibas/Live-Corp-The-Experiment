using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private Transform TurretHead;
    [SerializeField] private Transform PlayerTransform;
    [SerializeField] private float rotationSpeed;
    private void FixedUpdate()
    {
        if (PlayerTransform != null)
        {
            // Player'in konumunu ve turret'in konumunu alın
            Vector3 playerPosition = PlayerTransform.position;
            Vector3 turretPosition = TurretHead.position;

            // Player'in konumuna doğru bir vektör hesaplayın
            Vector3 direction = playerPosition - turretPosition;

            // Turret'in yönünü Player'in yönüne doğru döndürün
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            TurretHead.rotation = Quaternion.Lerp(TurretHead.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
