using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleController : MonoBehaviour
{
    [SerializeField] private ConsoleData _consoleData;
    public string test1;

    private void Start()
    {
        Debug.Log(_consoleData.HasContain(test1));
    }
}
