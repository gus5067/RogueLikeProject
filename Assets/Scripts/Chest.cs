using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Trap
{
    [SerializeField]
    private bool isTrap;

    [SerializeField]
    private GameObject mimic;
    public override void TrapActivate(Player player, int damage)
    {
        if (isTrap)
            Instantiate(mimic, transform.position, Quaternion.identity);
        else
            GameManager.Instance.Money += damage;

        gameObject.SetActive(false);
    }
}
