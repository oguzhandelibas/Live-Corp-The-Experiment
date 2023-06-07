using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioDatas", menuName = "AudioManagement/AudioDatas", order = 1)]
public class AudioDatas : ScriptableObject
{
    public string[] AudioText;
    public AudioClip[] AudioClips;
}