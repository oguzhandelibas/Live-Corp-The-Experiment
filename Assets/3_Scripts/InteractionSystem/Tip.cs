using UnityEngine;
using UnityEngine.Events;

public class Tip : MonoBehaviour, IInteractable
{
    public UnityEvent OnClick;
    public void InteractStart(GameObject interactObject, Transform parent)
    {
        OnClick?.Invoke();
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
