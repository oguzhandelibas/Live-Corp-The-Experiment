using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Leadboard
{
    [CreateAssetMenu(fileName = "TimeData", menuName = "PlayFab/TimeData", order = 1)]

    public class TimeData : ScriptableObject
    {
        public float BestTime;
    }
}
