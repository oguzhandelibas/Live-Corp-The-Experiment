using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Drag : MonoBehaviour, IInteractable
{
    public Transform holdParent;
    private GameObject heldObj;
    private Transform beforeHoldingTransform;
    
    public void InteractStart(GameObject interactObject)
    {
        if (heldObj == null)
        {
            PickupObject(interactObject);
        }
        else
        {
            DropObject();
        }
        UIManager.Instance.OpenTipPanel(TipPanelType.HackTip, true);
    }

    public void OnInteract()
    {
        throw new System.NotImplementedException();
    }

    public void InteractEnd()
    {
        throw new System.NotImplementedException();
    }
    
    
    void PickupObject(GameObject pickObj)
    {
        if (pickObj.GetComponent<Rigidbody>())
        {
            beforeHoldingTransform = pickObj.transform.parent;
            Rigidbody objRig = pickObj.GetComponent<Rigidbody>();
            objRig.useGravity = false;
            objRig.isKinematic = true;
            objRig.drag = 10;

            objRig.transform.parent = holdParent;
            
            objRig.transform.localRotation = Quaternion.Euler(0,0,0);
            objRig.transform.localPosition = new Vector3(0, 0, 5);
            
            heldObj = pickObj;

        }
    }
        
    void DropObject()
    {
        if (heldObj == null) return;
        Rigidbody heldRig = heldObj.GetComponent<Rigidbody>();
        heldRig.useGravity = true;
        heldRig.isKinematic = false;
        heldRig.drag = 1;
        heldObj.transform.SetParent(beforeHoldingTransform);
        //heldObj.transform.parent = null;
        heldObj = null;
    }

    
}
