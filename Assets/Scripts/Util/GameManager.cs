using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>//후에 제네릭 싱글톤으로 변경해야함
{
    [SerializeField] private int money;
    public int Money
    {
        get => money;
        set
        {
            money = value;
            OnChangeMoney?.Invoke(money);
        }
    }
    [SerializeField]
    private int playerHp;
    public int PlayerHp
    {
        get => playerHp;

        set
        {
            if (value > PlayerMaxHp)
                playerHp = PlayerMaxHp;
            else
                playerHp = value;
            OnChangeHp?.Invoke(playerHp);
        }
    }
    [SerializeField]
    private int playerMaxHp;
    public int PlayerMaxHp
    {
        get => playerMaxHp;
        set
        {
            playerMaxHp = value;
            playerHp = playerMaxHp;
        }
    }
    public int axeDamage;
    public float torchRange;

    public event UnityAction<int> OnChangeMoney;
    public event UnityAction<int> OnChangeHp;
    public int progressNum;

    [SerializeField] private int dungeonNum;
    public int DungeonNum
    {
        get { return dungeonNum; }
        set { dungeonNum = value; }
    }




}
