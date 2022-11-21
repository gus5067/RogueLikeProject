using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Servant_Dealer : Servant
{
    [SerializeField]
    private Weapon playerWeapon;

    private void Start()
    {
        playerWeapon.damage *= 2;
    }
}
