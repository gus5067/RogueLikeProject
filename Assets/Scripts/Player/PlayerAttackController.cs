using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour

{
    [SerializeField] private GameObject hitPoint;

    [SerializeField] private Weapon curWeapon;

    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            anim.SetTrigger("Attack");
        }
    }
    public void playerAttack()
    {
        curWeapon.Attack();
    }
}
