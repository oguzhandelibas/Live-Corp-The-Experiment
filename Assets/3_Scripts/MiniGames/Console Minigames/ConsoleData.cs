using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ConsoleData", menuName = "ScriptableObjects/ConsoleMiniGame/ConsoleData", order = 1)]
public class ConsoleData : ScriptableObject
{
   [SerializeField] private string[] HackCommands;
   public bool HasContain(string value) => HackCommands.Contains(value);
   public string HackCommandContent(int index) => HackCommands[index];
}
