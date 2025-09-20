using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MusicMgr : MonoSingleton<MusicMgr>
{
    AudioSource bgm;
    float bgmVolume;



    void Awake()
    {
        bgm = GetComponent<AudioSource>();
        bgmVolume = 1;
    }

    void OnEnable()
    {
    }

    void OnDisable()
    {
    }

    #region  Public Methods

    public void PlayBGM(MusicType musicType)
    {
        ResMgr.Instance.LoadRes<AudioClip>("Musics/" + musicType.ToString(), (clip) =>
        {
            bgm.clip = clip;
            bgm.loop = true;//循环播放
            bgm.volume = bgmVolume;
            bgm.Play();
        });
    }

    public void PauseBGM()
    {
        if (bgm) bgm.Pause();
    }

    public void StopBGM()
    {
        if (bgm) bgm.Stop();
    }

    public void ChangeBGMVolume(float volume)
    {
        //改变静态音量
        bgmVolume = volume;

        //改变动态音量
        if (bgm) bgm.volume = volume;
    }
    
    #endregion
}
