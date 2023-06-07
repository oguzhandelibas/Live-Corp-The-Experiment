using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private List<PlayList> PlayList = new List<PlayList>();

    /// <summary>
    /// Calls AudioClip
    /// </summary>
    /// <param name="index"></param>
    /// <returns>Wait Time</returns>
    public float PlayAudioClip(int index = -1)
    {
        if (index >= 0)
        {
            if (audioSource.isPlaying)
            {
                print("Add List");
                PlayList.Add(new PlayList(index, AudioDatas.AudioClips[index]));
            }
            else
            {
                print("Play With Index");
                Play(AudioDatas.AudioClips[index], index);
            }
        }
        else
        {
            print("Play First");
            Play(PlayList[0].audioClip, PlayList[0].Index);
            PlayList.RemoveAt(0);
        }
        
        return audioSource.clip.length;
    }

    private void Play(AudioClip clip, int index)
    {
        print("Oyna");
        audioSource.clip = clip;
        audioSource.Play();
        SubtitleManager.Instance.SetSubText(AudioDatas.AudioText[index], audioSource.clip.length);
        Invoke("CheckPlayList", audioSource.clip.length);
    }

    private void CheckPlayList()
    {
        print("Kontrol Et");
        if (PlayList.Count > 0)
        {
            Play(PlayList[0].audioClip, PlayList[0].Index);
            PlayList.RemoveAt(0);
        }
    }
}
