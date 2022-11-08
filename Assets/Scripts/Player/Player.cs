using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour,IDamageable,IForceable
{
    [SerializeField] private int hp;
    public int Hp { get { return hp; } set { hp = value; } }

    private Rigidbody2D rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void HitDamage(int damage)
    {
       Hp -= damage;
        Debug.Log("데미지 받음");
    }

    public void TakeFoce(Vector2 dir, int power)
    {
        rb.AddForce(dir * power, ForceMode2D.Impulse);
    }
}
