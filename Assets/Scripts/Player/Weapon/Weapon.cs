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

    public int damage;

    [SerializeField]
    private int power;

    [SerializeField]
    private SpriteRenderer rightWeapon;

    [SerializeField]
    private WeaponItemData defaultWeapon;

    [SerializeField]
    private GameObject weaponSlash;

    private void OnEnable()
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

    public void TileAttack()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(hitPos.transform.position, hitArea, 0f, 1<<10);
        if (colliders.Length > 0)
        {
           foreach(var col in colliders)
            {
                FragileWall wall = col.GetComponent<FragileWall>();
                if(wall != null)
                {
                    wall.BreakWall(hitPos.transform.position);
                }
            }
        }
    }
    public void ShowSlash()
    {
        weaponSlash.SetActive(true);
        weaponSlash.transform.position = hitPos.transform.position;
    }
    private void OnDrawGizmosSelected()
    {
        if (hitPos == null)
            return;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(hitPos.transform.position, hitArea);
    }

    public void ShowOffSlash()
    {
        weaponSlash.SetActive(false);
    }
}
