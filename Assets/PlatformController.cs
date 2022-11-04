using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] Animator anim;

    [SerializeField, Range(1f, 10f)]
    private float speed;
    public float Speed { get { return speed; } }

    [SerializeField]
    float inputX;
    [SerializeField]
    float inputY;

    float initScaleX;

    [SerializeField] RectTransform hitPoint;
}
