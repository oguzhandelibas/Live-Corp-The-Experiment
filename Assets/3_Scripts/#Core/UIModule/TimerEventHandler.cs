using System;
using UnityEngine;
using UnityEngine.Events;

public class TimerEventHandler : MonoBehaviour
{
    [Header("Timer Events")] 
    public UnityEvent OnTimerStart;
    public UnityEvent OnTimerEnd;
}
