using System;
using MiniGame.DoorGame;
using MiniGame.YesYes;
using NPC;
using Player;
using UnityEngine;
using UnityEngine.UI;


public class InteractionToItem : MonoBehaviour
{
    [SerializeField] private Transform lookPoint;
    [SerializeField] private float InteractRange;

    private Ray r;
    private IInteractable Interactable;
    private void Update()
    {
        r = new Ray(lookPoint.transform.position, lookPoint.transform.forward * InteractRange);
        if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out ChoicePlatform choicePlatform))
            {
                if(!choicePlatform.yes) choicePlatform.Change();
            }
            
            
            if (Interactable==null && hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
            {
                Interactable = interactObj;
                PlayerController.Instance.SetCrosshairColor(Color.green);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactObj.InteractStart(hitInfo.collider.gameObject, lookPoint);
                }
            }
            else if (hitInfo.collider.tag == "Target" ||hitInfo.collider.GetComponent<WoodBreak>())
            {
                PlayerController.Instance.SetCrosshairColor(Color.red);
            }
            else
            {
                PlayerController.Instance.SetCrosshairColor(Color.white);
            }
        }
        else
        {
            PlayerController.Instance.SetCrosshairColor(Color.white);
        }
        
        if (Interactable != null)
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                Interactable.InteractEnd();
            }
            else
            {
                Interactable = null;
            }
        }
    }
    
    
}
