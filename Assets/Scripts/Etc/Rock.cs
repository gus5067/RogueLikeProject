using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Rock_Type
{
    Red, Gold, Normal
}
public class Rock : DestroyObject
{
    [SerializeField] Slider slider;
    SpriteRenderer render;
    private float initHp;
    private int rewardMoney;
    [SerializeField]
    private bool isRandomValue;
    [SerializeField]
    private float rockHp;
    public float RockHp
    {
        get { return rockHp; }
        set
        {
            if (value < 0)
            {
                DestroyObj();
            }
            else
            {
                rockHp = value;
            }
        }
    }
    [SerializeField]
    Rock_Type rockType;
    Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        SetRockType();
        initHp = RockHp;
    }

    public void SetRockType()
    {
        if (isRandomValue)
        {
            int num = Random.Range(0, 100);

            if (num > 90)
            {
                rockType = Rock_Type.Red;
            }
            else if (num > 60)
            {
                rockType = Rock_Type.Gold;
            }
            else
            {
                rockType = Rock_Type.Normal;
            }
        }

        switch (rockType)
        {
            case Rock_Type.Normal:
                render.color = Color.white;
                rockHp = 2;
                rewardMoney = 50;
                break;
            case Rock_Type.Gold:
                render.color = Color.yellow;
                rockHp = 10;
                rewardMoney = 500;
                break;
            case Rock_Type.Red:
                render.color = Color.red;
                rockHp = 25;
                rewardMoney = 2000;
                break;
        }
    }

    public override void DestroyObj()
    {
       
        GameManager.Instance.Money += rewardMoney;
        Invoke("RespawnRock", Random.Range(5, 10));
        gameObject.SetActive(false);
    }

    public override void HitDamage(int damage)
    {
        Debug.Log("데미지 입음");
        RockHp -= damage;
        anim.SetTrigger("Hit");
        if(!slider.gameObject.activeSelf)
        {
            slider.gameObject.SetActive(true);
        }
        slider.value =RockHp / initHp;
    }

    public void RespawnRock()
    {

        gameObject.SetActive(true);
        slider.gameObject.SetActive(false);
        SetRockType();
        initHp = RockHp;

    }
}
