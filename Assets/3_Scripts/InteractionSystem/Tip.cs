using UnityEngine;
using UnityEngine.Events;

public class Tip : MonoBehaviour, IInteractable
{
    public void InteractStart(GameObject interactObject, Transform parent)
    {
        UIManager.Instance.Show<TipPanel>();
    }

    public void OnInteract()
    {
        
    }

    public void InteractEnd()
    {
        UIManager.Instance.Show<PlayerPanel>();
    }
}
