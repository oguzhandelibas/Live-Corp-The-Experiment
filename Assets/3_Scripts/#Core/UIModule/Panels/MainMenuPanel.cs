using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPanel : View
{
    [SerializeField] private GameObject gamePrefab;
    
    
    #region UI BUTTONS

    public void _CreateGame(){

        BootLoader.Instance.CreateGameLevel(gamePrefab);
        UIManager.Instance.Show<PlayerPanel>();
    }

    #endregion
    
}
