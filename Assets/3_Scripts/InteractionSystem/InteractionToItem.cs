using System;
using MiniGame.DoorGame;
using MiniGame.YesYes;
using UnityEngine;
using UnityEngine.UI;


public class InteractionToItem : MonoBehaviour
{
    [SerializeField] private Transform lookPoint;
    [SerializeField] private Image Crosshair;
    [SerializeField] private float InteractRange;

    private Ray r;
    private IInteractable Interactable;
    private void Update()
    {
        r = new Ray(lookPoint.transform.position, lookPoint.transform.forward * InteractRange);
        Gizmos.color = Color.red;
        Debug.DrawRay(lookPoint.transform.position, lookPoint.transform.forward * InteractRange);
        if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out ChoicePlatform choicePlatform))
            {
                if(!choicePlatform.yes) choicePlatform.Change();
            }
            
            if (Interactable==null && hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
            {
                Interactable = interactObj;
                Crosshair.color = Color.green;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactObj.InteractStart(hitInfo.collider.gameObject, lookPoint);
                    Crosshair.enabled = !Crosshair.enabled;
                }
            }
            else if(hitInfo.collider.gameObject.TryGetComponent(out WoodBreak woodBreak)) Crosshair.color = Color.red;
            else
            {
                Crosshair.color = Color.white;
            }
        }
        if (Interactable != null)
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                Interactable.InteractEnd();
                Crosshair.enabled = true;
            }
            else
            {
                Interactable = null;
            }
        }
    }
    
    
}
