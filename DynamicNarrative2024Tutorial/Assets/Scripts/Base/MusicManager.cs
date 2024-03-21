using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 声音管理器
/// 采用单例
/// </summary>
public class MusicManager : BaseManager<MusicManager>
{
    private List<AudioSource> soundList = new List<AudioSource>();

    Dictionary<string, AudioClip> soundDic = new Dictionary<string, AudioClip>();

    private float soundValue = 1;

    // cache
    private string soundName;


    public MusicManager()
    {
        MonoManager.GetInstance().AddUpdateListener(Update);
        for (int i = 0; i < 11; ++i)
        {
            PoolManager.GetInstance().GetObject("Sound/SoundPlayer",
                (res => { PoolManager.GetInstance().PushObject("Sound/SoundPlayer", res); }));
        }
    }

    public void Update()
    {
        for (int i = soundList.Count - 1; i >= 0; --i)
        {
            if (soundList[i] != null && !soundList[i].isPlaying)
            {
                PoolManager.GetInstance().PushObject("Sound/SoundPlayer", soundList[i].gameObject);
                soundList.RemoveAt(i);
            }

        }

        for (int i = soundList.Count - 1; i >= 0; --i)
        {
            if (soundList[i] == null)
            {
                soundList.RemoveAt(i);
                Debug.Log("删了一个声音");

            }
        }
    }

    /// <summary>
    /// 播放
    /// </summary>
    public void PlaySound(string name, bool isLoop = false,  float volume = 1)
    {
        foreach (AudioSource sound in soundList)
        {
            if (sound.clip.name.Equals(name) && sound.isPlaying)
            {
                return;
            }
        }


        PoolManager.GetInstance().GetObject("Sound/SoundPlayer", (bgmPlayer) =>
        {
            AudioSource source = bgmPlayer.GetComponent<AudioSource>();
            source.volume = soundValue;
            source.loop = isLoop;
            source.volume = volume;
            if (soundDic.ContainsKey(name))
            {
                source.clip = soundDic[name];
                source.Play();
                soundList.Add(source);
            }
            else
            {
                try
                {
                    ResManager.GetInstance().LoadAsync<AudioClip>("Sound/" + name, (audio) =>
                    {
                        source.clip = audio;
                        soundDic.Add(name, audio);
                        source.Play();
                        soundList.Add(source);
                    });
                }
                finally { }
            }
        });
    }

    /// <summary>
    /// 暂停
    /// </summary>
    public void PauseSound(string name)
    {
        foreach (AudioSource sound in soundList)
        {
            if (sound.clip.name.Equals(name))
            {
                sound.Pause();
            }
        }
    }

    /// <summary>
    /// 停止
    /// </summary>
    public void StopSound(string name)
    {
        for (int i = 0; i < soundList.Count; ++i)
        {
            if (soundList[i].clip.name.Equals(name))
            {
                soundList[i].Stop();
            }
        }
    }

    /// <summary>
    /// 重新播放
    /// </summary>
    /// <param name="name"></param>
    public void RestartAllSound(string name)
    {
        
    }

    public void RestartSound(string name)
    {
        
    }

    public void ReplaySound(string name)
    {
        
    }

    /// <summary>
    /// 修改音量
    /// </summary>
    /// <param name="v">0~1</param>
    public void SetAllSoundValue(float newVolume)
    {
        
    }

    public void SetSoundValue(string name, float newVolume)
    {
        
    }

    /// <summary>
    /// 停止所有
    /// </summary>
    public void StopAllSound()
    {
        
    }

    /// <summary>
    /// 暂停所有
    /// </summary>
    public void PauseAllSound()
    {
        foreach (AudioSource sound in soundList)
        {
            if (sound != null)
            {
                sound.Stop();
            }
        }
    }


    /// <summary>
    /// 重新播放所有的SE和BGM，对应Setting面板的退出
    /// </summary>
    public void UnPauseAllSound()
    {
        
    }

   
    public float GetVolume()
    {
        return soundValue;
    }



   
}