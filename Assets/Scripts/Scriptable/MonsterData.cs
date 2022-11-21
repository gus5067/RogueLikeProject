using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu,Serializable]
public class MonsterData : ScriptableObject
{
    public new string name;
    public int hp;
    public float speed;
    [HideInInspector]
    public float viewRadius = 5f;
    public int damage;
    public Sprite sprite;
    public AnimatorController animator;
    public Moster_AttackType attackType;
    public AudioClip audioClip;

}
