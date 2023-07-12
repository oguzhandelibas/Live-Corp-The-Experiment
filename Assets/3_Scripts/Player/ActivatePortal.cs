using System;
using DG.Tweening;
using UnityEngine;

public class ActivatePortal : MonoBehaviour
{
    [SerializeField] private GameObject portal;

    private void Start()
    {
        Deactivate();
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        Vector3 targetPos = portal.transform.localPosition + (Vector3.up*3);
        portal.transform.DOLocalMove(targetPos, 1);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
        Vector3 targetPos = portal.transform.localPosition + (Vector3.down*3);
        portal.transform.DOLocalMove(targetPos, 1);
    }
}
