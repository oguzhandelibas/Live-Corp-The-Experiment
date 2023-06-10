using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPanel : View
{
    #region UI BUTTONS

    public void _CreateGame(){
        BootLoader.Instance.CreateGameLevel();
    }

    #endregion
    
}
