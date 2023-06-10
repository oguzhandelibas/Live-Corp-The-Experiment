using UnityEngine;

public interface IInteractable
{
    public void InteractStart(GameObject interactObject, Transform parent);
    public void OnInteract();
    public void InteractEnd();
}
