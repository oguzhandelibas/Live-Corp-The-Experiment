using UnityEngine;

public interface IInteractable
{
    public void InteractStart(GameObject interactObject);
    public void OnInteract();
    public void InteractEnd();
}
