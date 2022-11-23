using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField]
    private int damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                TrapActivate(player, damage);
            }
        }
    }
   

    public virtual void TrapActivate(Player player, int damage)
    {
        player.HitDamage(damage);
    }

}
