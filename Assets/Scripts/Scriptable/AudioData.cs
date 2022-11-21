using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class AudioData : ScriptableObject
{
    public Sound[] sfx;
    public Sound[] bgms;
}

[Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    public float volume;
}
