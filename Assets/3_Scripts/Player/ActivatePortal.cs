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
        Debug.Log("Activated");
        Vector3 targetPos = portal.transform.localPosition + (Vector3.up*3);
        portal.transform.DOLocalMove(targetPos, 1);
    }

    public void Deactivate()
    {
        Debug.Log("Deactivated");
        Vector3 targetPos = portal.transform.localPosition + (Vector3.down*3);
        portal.transform.DOLocalMove(targetPos, 1);
    }
}
