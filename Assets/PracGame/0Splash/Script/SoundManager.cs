using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System;
using System.Linq;

[Serializable]
public class AudioClipDic
{
    public string audioClipName;
    public AudioClip audioClip;
}

public class SoundManager : Singleton<SoundManager>
{
    public enum SoundType
    {
        BGM = 0,
        Sfx = 1,
        UI = 2,
        LoopSfx = 3
    }

    [Header("Audio Mixer")]
    public AudioMixer audioMixer;

    [Header("Audio Source")]
    public AudioSource bgM;
    public AudioSource sfxM;
    public AudioSource uiM;
    public AudioSource LoopSfxM;

    [Header("Audio Clip")]
    public List<AudioClipDic> audioClipList;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        //Debug.Log("arg0 : " + arg0);
        //Debug.Log("arg0 buildIndex : " + arg0.buildIndex);
        //Debug.Log("arg0 name : " + arg0.name);

        //if (arg0.buildIndex.ToString().Equals("1") || arg0.buildIndex.ToString().Equals("4") || arg0.buildIndex.ToString().Equals("6") || arg0.buildIndex.ToString().Equals("10"))
        //{
        //    //PlayBGMByKey("SelectStoryScene");
        //    StopBGM();
        //}
        //else
        //{
        //    for (int i = 0; i < audioClipList.Count; i++)
        //    {
        //        if (arg0.name.Equals(audioClipList[i].audioClipName))
        //        {
        //            PlayBGMByClip(audioClipList[i].audioClip);
        //            break;
        //        }
        //    }
        //}
    }

    public AudioClip GetAudioClip(string pClipNameKey)
    {
        if (pClipNameKey == "None") return null;

        AudioClipDic tempClipDic = audioClipList.Where(temp => temp.audioClipName == pClipNameKey).FirstOrDefault();

        if (tempClipDic == null)
        {
            return null;
        }
        else
        {
            return tempClipDic.audioClip;
        }
    }

    public void AddAudioClip(string pClipNameKey, AudioClip pAudioClip)
    {
        AudioClipDic tempClip = new AudioClipDic();
        tempClip.audioClipName = pClipNameKey;
        tempClip.audioClip = pAudioClip;
        audioClipList.Add(tempClip);
    }

    //옵션을 변경할 때 소리의 불륨을 조절하는 함수
    public void SetVolume(SoundType type, float value)
    {
        audioMixer.SetFloat(type.ToString(), value);
    }

    public void PlayBGMByKey(string pClipKey)
    {
        AudioClip tempClip = GetAudioClip(pClipKey);
        if (tempClip != null)
        {
            bgM.clip = tempClip;
            bgM.Play();
        }
    }

    public void SetBGMByKey(string pClipKey)
    {
        AudioClip tempClip = GetAudioClip(pClipKey);
        if (tempClip != null)
        {
            bgM.clip = tempClip;
        }
    }

    public AudioSource GetBGMSource()
    {
        return bgM;
    }

    public void PlayBGMByClip(AudioClip pClip)
    {
        bgM.clip = pClip;
        bgM.Play();
    }

    public void StopBGM()
    {
        bgM.Stop();
    }

    public void PlaySFXByKey(string pClipKey)
    {
        AudioClip tempClip = GetAudioClip(pClipKey);
        if (tempClip != null)
        {
            sfxM.clip = tempClip;
            sfxM.Play();
        }
    }
    public void PlaySFXByClip(AudioClip pClip)
    {
        sfxM.clip = pClip;
        sfxM.Play();
    }
    public void StopSfx()
    {
        sfxM.Stop();
    }

    public void PlayUIMByKey(string pClipKey)
    {
        AudioClip tempClip = GetAudioClip(pClipKey);
        if (tempClip != null)
        {
            uiM.clip = tempClip;
            uiM.Play();
        }
    }
    public void PlayUIMByClip(AudioClip pClip)
    {
        uiM.clip = pClip;
        uiM.Play();
    }
    public void StopUIM()
    {
        uiM.Stop();
    }

    public void PlayLoopSFXByKey(string pClipKey)
    {
        AudioClip tempClip = GetAudioClip(pClipKey);
        if (tempClip != null)
        {
            LoopSfxM.clip = tempClip;
            LoopSfxM.Play();
        }
    }
    public void PlayLoopSFXByClip(AudioClip pClip)
    {
        LoopSfxM.clip = pClip;
        LoopSfxM.Play();
    }
    public void StopLoopSfx()
    {
        LoopSfxM.Stop();
    }

    public void SubPlay(string subName, AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogError("호출한 자막 사운드 파일이 사운드 매니져에 존재하지 않습니다.");
            return;
        }
        GameObject go = new GameObject(subName + "Sound");
        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Sub")[0];
        audioSource.clip = clip;
        audioSource.Play();

        Destroy(go, clip.length);
    }



    public AudioSource GetAudioSource(string subName, AudioClip clip)
    {
        GameObject go = new GameObject(subName + "Sound");
        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("BGM")[0];
        audioSource.clip = clip;
        //audioSource.Play();

        return audioSource;
    }
    public AudioSource GetAudioSource(string subName, string pClipKey)
    {

        GameObject go = new GameObject(subName + "Sound");
        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("BGM")[0];
        AudioClip tempClip = GetAudioClip(pClipKey);
        if (tempClip != null)
        {
            audioSource.clip = tempClip;
        }
        return audioSource;
    }
}
