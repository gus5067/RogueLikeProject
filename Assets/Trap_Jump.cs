using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Jump : Trap
{
    public override void TrapActivate(Player player, int damage)
    {
        GameManager.Instance.Money -= damage;
        if (GameManager.Instance.Money < -1000)
            LoadManager.LoadScene("MiningCaveScene");
        else
            LoadManager.LoadScene("TownScene");
    }
}
