using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : Weapon
{

    [SerializeField] Transform hitPoint;
    public override void Attack()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(hitPoint.position, Vector2.one * 2, 0);
        foreach (var col in colliders)
        {
            IDamageable damageTarget = col.GetComponent<IDamageable>();
            damageTarget?.HitDamage(1);
        }
    }
}
