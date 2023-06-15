using DG.Tweening;
using UnityEngine;
using BreakableBox;

public class Drag : MonoBehaviour, IInteractable
{
    private GameObject heldObj;
    private Transform beforeHoldingTransform;
    
    public void InteractStart(GameObject interactObject, Transform parent)
    {
        if (heldObj == null)
        {
            if (transform.TryGetComponent(out Destructible destructible)) destructible.CanBreak = true;
            PickupObject(interactObject, parent);
        }
        else
        {
            DropObject();
        }
    }

    public void OnInteract()
    {
        
    }

    public void InteractEnd()
    {
        
    }
    
    
    void PickupObject(GameObject pickObj, Transform parent)
    {
        if (pickObj.GetComponent<Rigidbody>())
        {
            beforeHoldingTransform = pickObj.transform.parent;
            
            Rigidbody objRig = pickObj.GetComponent<Rigidbody>();
            Transform objectTransform = pickObj.transform;
            
            objRig.useGravity = false;
            objRig.isKinematic = true;
            objRig.drag = 10;

            objectTransform.parent = parent;
            objectTransform.localRotation = Quaternion.Euler(0,0,0);
            objectTransform.localPosition = new Vector3(0, 0, 2);
            objectTransform.DORotate(new Vector3(5, 5, 5), 1);
            
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
        heldObj = null;
    }

    
}
