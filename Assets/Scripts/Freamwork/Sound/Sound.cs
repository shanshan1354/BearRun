using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoSingleton<Sound>
{

    AudioSource bg;
    AudioSource audioEffect;
    public string resourcesDir = "";

    protected override void Awake()
    {
        base.Awake();
        bg = gameObject.AddComponent<AudioSource>();
        bg.playOnAwake = false;
        bg.loop = true;
        audioEffect = gameObject.AddComponent<AudioSource>();
    }

    //切换背景音乐
    public void PlayBG(string bgPath)
    {
        string path = resourcesDir + "/" + bgPath;
        AudioClip bgClip = Resources.Load<AudioClip>(path);
        if (bgClip != null)
        {
            if (bg.clip != bgClip)
            {
                bg.clip = bgClip;
            }
            bg.Play();
        }
    }

    //播放音效
    public void PlayAudioEffect(string audioPath)
    {
        string path = resourcesDir + "/" + audioPath;
        AudioClip audioClip = Resources.Load<AudioClip>(path);
        if (audioClip != null)
        {
            audioEffect.PlayOneShot(audioClip);
        }
    }
}
