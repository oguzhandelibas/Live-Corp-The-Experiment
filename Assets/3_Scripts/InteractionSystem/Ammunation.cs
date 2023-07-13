using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class Ammunation : MonoBehaviour, IInteractable
{
    [SerializeField] private int bulletCount;
    public void InteractStart(GameObject interactObject, Transform parent)
    {
        PlayerController.Instance.TakeAmmunation(bulletCount);
        Destroy(gameObject);
    }

    public void OnInteract()
    {
    }

    public void InteractEnd()
    {
    }
}
