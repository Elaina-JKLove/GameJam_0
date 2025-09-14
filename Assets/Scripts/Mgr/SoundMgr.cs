using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundMgr : MonoSingleton<SoundMgr>
{
    List<AudioSource> soundList;//同一时间音效不唯一
    float soundVolume;



    void Awake()
    {
        soundList = new();
        soundVolume = 1;
    }

    void OnEnable()
    {
    }

    void Update()
    {
        RemovePlayedSounds();
    }

    void OnDisable()
    {
    }

    #region  Public Methods

    public void PlaySound(SoundType soundType)
    {
        ResMgr.Instance.LoadRes<AudioClip>("Sound/" + soundType.ToString(), (clip) =>
        {
            AudioSource sound = this.AddComponent<AudioSource>();
            sound.clip = clip;
            sound.loop = false;//不循环播放
            sound.volume = soundVolume;
            sound.Play();

            soundList.Add(sound);
        });
    }

    public void StopSound(AudioSource audioSource)
    {
        if (!soundList.Contains(audioSource)) return;

        audioSource.Stop();

        soundList.Remove(audioSource);
        Destroy(audioSource);
    }

    public void ChangeSoundVolume(float volume)
    {
        //改变静态音量
        soundVolume = volume;

        //改变动态音量
        for (int i = 0; i < soundList.Count; i++)
        {
            soundList[i].volume = volume;
        }
    }

    #endregion

    #region  Private Methods

    //帧检测播放完毕的音效组件销毁
    void RemovePlayedSounds()
    {
        for (int i = soundList.Count - 1; i >= 0; i--)
        {
            if (!soundList[i].isPlaying)
            {
                soundList.RemoveAt(i);
                Destroy(soundList[i]);
            }
        }
    }

    #endregion
}
