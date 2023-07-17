using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPanel : View
{
    #region UI BUTTONS

    public void _CreateGame(){
        BootLoader.Instance.CreateGameLevel();
    }

    public void _Credit()
    {
        UIManager.Instance.Show<CreditPanel>();
    }

    public void _Quit()
    {
        Application.Quit();  
    }

    #endregion
    
}
