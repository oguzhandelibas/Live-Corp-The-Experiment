using System;
using UnityEngine;


public class InteractionToItem : MonoBehaviour
{
    [SerializeField] private SpriteRenderer Crosshair;
    [SerializeField] private float InteractRange;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray r = new Ray(Crosshair.transform.position, Crosshair.transform.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    interactObj.Interact();
                }
            }
        }
    }
}
