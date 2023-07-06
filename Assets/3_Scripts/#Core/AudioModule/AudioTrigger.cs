using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    [SerializeField] private Transform audioManagerPosition;
    [SerializeField] private int[] AudioIndex;
    private BoxCollider boxCollider;

    private void OnEnable()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (var item in AudioIndex)
        {
            AudioManager.Instance.PlayAudioClip(audioManagerPosition, item);
        }
        boxCollider.enabled = false;

    }
}
