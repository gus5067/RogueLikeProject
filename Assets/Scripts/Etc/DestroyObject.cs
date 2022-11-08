using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DestroyObject : MonoBehaviour,IDamageable
{

    public abstract void DestroyObj();
    public abstract void HitDamage(int damage);
}
