using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTest : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Destroy(gameObject);
    }
}
