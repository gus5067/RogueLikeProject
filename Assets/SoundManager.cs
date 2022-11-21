using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : Singleton<SoundManager>
{
    public AudioData data;
    private AudioSource au;
    private new void Awake()
    {
        base.Awake();
        au = GetComponent<AudioSource>();
        SceneManager.sceneLoaded += BgmPlay;
    }
    public void BgmPlay(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "LoadingScene")
            return;
        for(int i = 0; i<data.bgms.Length;i++)
        {
            if (scene.name == data.bgms[i].name)
            {
                au.clip = data.bgms[i].clip;
                au.volume = data.bgms[i].volume;
                au.Play();
                return;
            }            
        }
    }

    public void AudioPlay(string name, AudioClip clip)
    {
        GameObject effect = new GameObject(name + " Sound");
        AudioSource source = effect.AddComponent<AudioSource>();
        source.clip = clip;
        source.Play();

        Destroy(effect, clip.length);
    }
}
