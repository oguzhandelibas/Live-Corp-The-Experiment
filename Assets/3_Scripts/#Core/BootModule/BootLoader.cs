using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootLoader : AbstractSingleton<BootLoader>
{
    private GameObject mainMenu;
    private GameObject game;

    private void Start()
    {
        mainMenu = transform.GetChild(0).gameObject;
    }
        
    public void CreateGameLevel(GameObject referenceObject)
    {
        var gameObj = Instantiate(referenceObject);
        gameObj.name = "--->" + gameObj.name;
        mainMenu.SetActive(true);
    }
        
    private void DeactivateMenu(GameObject gameObj)
    {
        game = gameObj;
        mainMenu.SetActive(false);
    }
    
}
