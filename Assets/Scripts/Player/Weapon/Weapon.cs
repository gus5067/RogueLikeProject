using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private Transform hitPos;

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

    [SerializeField]
    private WeaponItemData defaultWeapon;

    private void Start()
    {
        if (EquipManager.Instance.curWeaponData != null)
            WeaponSet(EquipManager.Instance.curWeaponData);
        else
            WeaponSet(defaultWeapon);
    }

    public void WeaponSet(WeaponItemData data)
    {
        this.sprite = data.icon;
        rightWeapon.sprite = this.sprite;
        this.hitArea = data.hitArea;
        this.damage = data.value;
        this.power = data.power;
    }

    public virtual void Attack()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(hitPos.transform.position, hitArea, 0f);
        if (colliders.Length > 0)
        {
           
            foreach (var col in colliders)
            {
                Monster target = null;
                target = col.GetComponent<Monster>();
                target?.HitDamage(damage);
                target?.TakeForce((col.transform.position - transform.position).normalized, power);
            }
        }
    }
}
