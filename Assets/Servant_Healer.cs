using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Servant_Healer : Servant
{
    [SerializeField] private int heal;
    [SerializeField] Player playerHp;
    [SerializeField] private int healCount;
    [SerializeField] private int healTime;

    private void Start()
    {
        StartCoroutine(HealRoutine());
    }
    IEnumerator HealRoutine()
    {
       for(int i = 0; i < healCount; i++)
        {
            playerHp.Hp += heal;
            yield return new WaitForSeconds(healTime);
        }
    }
}
