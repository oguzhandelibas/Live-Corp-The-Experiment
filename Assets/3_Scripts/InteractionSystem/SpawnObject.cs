using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    
    public void Spawn()
    {
        Instantiate(prefab, transform.position, Quaternion.identity, transform.parent);
    }
}
