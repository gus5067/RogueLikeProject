using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{


    [SerializeField]
    private Transform hitPos;

    [SerializeField]
    private Vector2 hitArea;

    [SerializeField]
    private int damage;

    [SerializeField]
    private int power;


    public override void Attack()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(hitPos.transform.position, hitArea, 0f);
        if (colliders.Length > 0)
        {
            foreach (var col in colliders)
            {
                Monster mons = col.GetComponent<Monster>();
                mons?.HitDamage(damage);
                mons?.TakeForce((col.transform.position - transform.position).normalized, power);
            }
        }
    }
}
