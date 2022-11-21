using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Servant_Dealer : Servant
{
    [SerializeField]
    private Weapon playerWeapon;

    private new void Start()
    {
        base.Start();
        playerWeapon.damage *= 2;
    }
}
