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
    
    public void AddAudioClip(AudioTrigger _audioTrigger, int[] indexes)
    {
        audioTrigger = _audioTrigger;
        foreach (var item in indexes)
        {
            PlayList.Add(new PlayList(item, AudioDatas.AudioClips[item]));
        }

        PlayAudioClip(indexes[0]);
    }

    public void AddAudioClip(int[] indexes)
    {
        foreach (var item in indexes)
        {
            PlayList.Add(new PlayList(item, AudioDatas.AudioClips[item]));
        }
        audioTrigger = null;   
        PlayAudioClip(indexes[0]);
    }

    public void SetSpeakerPosition(Transform target)
    {
        transform.position = target.position;
    }
    
    /// <summary>
    /// Calls AudioClip
    /// </summary>
    /// <param name="index"></param>
    /// <returns>Wait Time</returns>
    public float PlayAudioClip(int index = -1)
    {
        if (audioSource.isPlaying) return 0;
        totalSoundCount = PlayList.Count;
        
        if (index >= 0)
        {
            CheckPlayList();
        }
        else
        {
            Play(PlayList[0].audioClip, PlayList[0].Index);
            PlayList.RemoveAt(0);
        }
        
        totalSoundCount = PlayList.Count;
        return audioSource.clip.length;
    }
    
    private void CheckPlayList()
    {
        if (PlayList.Count > 0)
        {
            Play(PlayList[0].audioClip, PlayList[0].Index);
            PlayList.RemoveAt(0);
        }
    }

    private void Play(AudioClip clip, int index)
    {
        audioSource.clip = clip;
        audioSource.Play();
        bool lastSound = PlayList.Count <= 1;
        //(totalSoundCount-1 == PlayList[0].Index) || 

        SubtitleManager.Instance.SetSubText(audioTrigger, AudioDatas.AudioText[index], audioSource.clip.length, lastSound);
        Invoke("CheckPlayList", audioSource.clip.length);
    }

    public void ResetSound()
    {
        SubtitleManager.Instance.ResetSubtitle();
        PlayList.Clear();
        audioSource.Stop();
    }
}
