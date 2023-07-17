using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditPanel : View
{
    public void _MainMenu()
    {
        UIManager.Instance.Show<MainMenuPanel>();
    }
}
