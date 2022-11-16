using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>//�Ŀ� ���׸� �̱������� �����ؾ���
{
    [SerializeField] private int money;
    public int Money
    {
        get => money;
        set
        {
            money = value;
            onMoneyChange?.Invoke(money);
        }
    }

    public int playerHp;
    public int playerMaxHp;

    public float torchRange;

    public event UnityAction<int> onMoneyChange;

    [SerializeField] private int dungeonNum;
    public int DungeonNum
    {
        get { return dungeonNum; }
        set { dungeonNum = value; }
    }




}
