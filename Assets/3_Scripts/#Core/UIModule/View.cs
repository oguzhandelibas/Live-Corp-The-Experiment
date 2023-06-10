using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    /// <summary>
    /// Initializes the View
    /// </summary>
    public virtual void Initialize()
    {
    }

    #region UI BUTTON

    public void _ClosePanel()
    {
        UIManager.Instance.GoBack();
    }

    #endregion
        
        
    /// <summary>
    /// Makes the View visible
    /// </summary>
    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Hides the view
    /// </summary>
    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}
