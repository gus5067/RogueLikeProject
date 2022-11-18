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
            OnMoneyChange?.Invoke(money);
        }
    }

    public int playerHp;
    public int playerMaxHp;

    public float torchRange;

    public event UnityAction<int> OnMoneyChange;

    public int progressNum;

    [SerializeField] private int dungeonNum;
    public int DungeonNum
    {
        get { return dungeonNum; }
        set { dungeonNum = value; }
    }




}
