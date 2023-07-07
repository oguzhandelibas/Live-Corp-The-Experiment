using System;
using DG.Tweening;
using UnityEngine;

public class ActivatePortal : MonoBehaviour
{
    [SerializeField] private GameObject portal;

    private void Start()
    {
        portal.transform.position += (Vector3.down*3);
    }

    public void Activate()
    {
        Vector3 targetPos = portal.transform.position + (Vector3.up*3);
        portal.transform.DOLocalMove(targetPos, 1);
    }

    public void Deactivate()
    {
        Vector3 targetPos = portal.transform.position + (Vector3.down*3);
        portal.transform.DOLocalMove(targetPos, 1);
    }
}
