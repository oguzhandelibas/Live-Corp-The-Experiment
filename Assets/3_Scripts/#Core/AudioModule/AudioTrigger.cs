using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    [SerializeField] private int AudioIndex;
    private BoxCollider boxCollider;

    private void OnEnable()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        AudioManager.Instance.PlayAudioClip(AudioIndex);
        //boxCollider.enabled = false;

    }
}
