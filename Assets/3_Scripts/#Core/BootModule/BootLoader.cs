using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootLoader : AbstractSingleton<BootLoader>
{
    [SerializeField] private GameObject gamePrefab;

    public void CreateGameLevel()
    {
        var gameObj = Instantiate(gamePrefab);
        gameObj.name = "--->" + gameObj.name;
        UIManager.Instance.Show<PlayerPanel>();
        
    }

}
