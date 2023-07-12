using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayList
{
    public int Index;
    public AudioClip audioClip;
    public PlayList(int _index, AudioClip _audioClip)
    {
        Index = _index;
        audioClip = _audioClip;
    }
}

public class AudioManager : AbstractSingleton<AudioManager>
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioDatas AudioDatas;
    [SerializeField] private List<PlayList> PlayList = new List<PlayList>();
    private int totalSoundCount = 0;
    private AudioTrigger audioTrigger;
    
    public void AddAudioClip(AudioTrigger _audioTrigger, int index = -1)
    {
        audioTrigger = _audioTrigger;
        if (index >= 0)
        {
            PlayList.Add(new PlayList(index, AudioDatas.AudioClips[index]));
        }

        totalSoundCount = PlayList.Count;
    }

    public void AddAudioClip(int index = -1)
    {
        if (index >= 0)
        {
            PlayList.Add(new PlayList(index, AudioDatas.AudioClips[index]));
        }
        totalSoundCount = PlayList.Count;

        if (!audioSource.isPlaying)
        {
            Play(PlayList[0].audioClip, PlayList[0].Index);
            PlayList.RemoveAt(0);
        }
    }
    
    /// <summary>
    /// Calls AudioClip
    /// </summary>
    /// <param name="index"></param>
    /// <returns>Wait Time</returns>
    public float PlayAudioClip(Transform target, int index = -1)
    {
        if (index >= 0)
        {
            CheckPlayList();
            transform.position = target.position;
        }
        else
        {
            Play(PlayList[0].audioClip, PlayList[0].Index);
            PlayList.RemoveAt(0);
        }
        
        return audioSource.clip.length;
    }

    private void Play(AudioClip clip, int index)
    {
        audioSource.clip = clip;
        audioSource.Play();
        bool lastSound = (totalSoundCount-1 == PlayList[0].Index) || PlayList.Count <= 1;
        SubtitleManager.Instance.SetSubText(audioTrigger, AudioDatas.AudioText[index], audioSource.clip.length, lastSound);
        Invoke("CheckPlayList", audioSource.clip.length);
    }

    private void CheckPlayList()
    {
        if (PlayList.Count > 0)
        {
            Play(PlayList[0].audioClip, PlayList[0].Index);
            PlayList.RemoveAt(0);
        }
    }
}
