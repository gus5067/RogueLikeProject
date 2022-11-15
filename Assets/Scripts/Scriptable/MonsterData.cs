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
    public float viewRadius;
    public int damage;
    public Sprite sprite;
    public AnimatorController animator;
    

}
