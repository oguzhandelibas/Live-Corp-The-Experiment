using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : AbstractSingleton<UIManager>
{
    public void OpenTipPanel(TipPanelType tipType, bool activeness)
    {
        Debug.Log(tipType.ToString() + " " + activeness);
    }
}
