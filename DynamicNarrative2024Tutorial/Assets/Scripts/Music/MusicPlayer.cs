using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    public SoundResource[] sounds;

    public static MusicPlayer Instance;

    private Dictionary<string, SoundResource> soundResDic = new Dictionary<string, SoundResource>();
    private Dictionary<SoundType, List<string>> soundTypeDic = new Dictionary<SoundType, List<string>>();

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            Instance = this;
        }
    }

    
    private void Start()
    {
        foreach(SoundResource sound in sounds)
        {
            if(sound.name != "" && sound.name != null)
            {
                if (!soundResDic.ContainsKey(sound.name))
                    //soundResDic[sound.name] = sound;
                    soundResDic.TryAdd(sound.name, sound);
            }


            if (sound.type != SoundType.Undefined)
            {
                if (!soundTypeDic.ContainsKey(sound.type))
                {
                    List<string> soundsInType = new List<string>();
                    soundTypeDic.TryAdd(sound.type, soundsInType);
                    //soundTypeDic[sound.type] = new List<string> ();
                }
                soundTypeDic[sound.type].Add(sound.name);
            }
    

            if (sound.autoPlay)
            {
                sound.Play();
            }    
        }

    }

    public void PlaySound(string soundName)
    {
        if (soundResDic.ContainsKey(soundName))
            soundResDic[soundName].Play();
    }

    public void PlaySound(SoundType soundType)
    {
        if(!soundTypeDic.ContainsKey(soundType))
        {
            Debug.LogError("Here is not sound in this Type: " + soundType.ToString());
            return;
        }

        // 一个类型下多个声音则随机播放
        System.Random random = new System.Random();
        int index = random.Next(soundTypeDic[soundType].Count);
        string randomSound = soundTypeDic[soundType][index];
        PlaySound(randomSound);
    }

    public void StopSound(string soundName)
    {
        if (soundResDic.ContainsKey(soundName))
            soundResDic[soundName].Stop();
    }

    public void StopSound(SoundType soundType)
    {
        if (!soundTypeDic.ContainsKey(soundType))
        {
            Debug.LogError("Here is not sound in this Type: " + soundType.ToString());
            return;
        }

        foreach(string soundName in soundTypeDic[soundType])
        {
            soundResDic[soundName].Stop();
        }
    }


    public void PlayDeathSound()
    {
        StopAllPlayerSound();
        PlaySound(SoundType.Death);
    }

    public void StopAllPlayerSound()
    {
        StopSound(SoundType.LowEnergy);
        StopSound(SoundType.Floating);
    }

    private void OnEnable()
    {
        PlayerStatusController.OnCharacterDeath += PlayDeathSound;
        PlayerStatusController.OnPlayerReset += StopAllPlayerSound;
    }

    private void OnDisable()
    {
        PlayerStatusController.OnCharacterDeath -= PlayDeathSound;
        PlayerStatusController.OnPlayerReset -= StopAllPlayerSound;
    }
}

[System.Serializable]
public class SoundResource
{
    public string name;
    public SoundType type;
    public float setVolume;
    public bool autoPlay;
    public bool loop;
    [HideInInspector] public float tempVolume;

    public SoundResource()
    {
        
    }

    public void Play()
    {
        MusicManager.GetInstance().PlaySound(name, loop, setVolume);
    }

    public void Stop()
    {
        MusicManager.GetInstance().StopSound(name);
    }
}

public enum SoundType
{
    Undefined,
    BGM,
    Move,
    Decelerate,
    Jump,
    Hit,
    Floating,
    ChargeEnergy,
    LowEnergy,
    Death,
    Win,
    Dialog,
}


