using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private Transform hitPos;

    [SerializeField]
    private WeaponItemData weaponItemData;

    [SerializeField]
    private Sprite sprite;

    [SerializeField]
    private Vector2 hitArea;

    [SerializeField]
    private int damage;

    [SerializeField]
    private int power;

    [SerializeField]
    private SpriteRenderer rightWeapon;

    private void Start()
    {
        WeaponInit(weaponItemData);
    }

    public void WeaponInit(WeaponItemData data)
    {
        this.sprite = data.icon;
        rightWeapon.sprite = this.sprite;
        this.hitArea = data.hitArea;
        this.damage = data.attackValue;
        this.power = data.power;
    }

    public virtual void Attack()
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
