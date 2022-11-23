using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Hole : Trap
{

    public override void TrapActivate(Player player, int damage)
    {
        base.TrapActivate(player, GameManager.Instance.PlayerHp/2);
        if(GameManager.Instance.DungeonNum > 4)
        {
            player.HitDamage(GameManager.Instance.PlayerMaxHp);
        }
        else
        {
            GameManager.Instance.DungeonNum++;
            LoadManager.LoadScene("DungeonScene");
        }
    }
}
