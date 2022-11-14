using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>//�Ŀ� ���׸� �̱������� �����ؾ���
{
    [SerializeField] private int money;
    public int Money
    {
        get => money;
        set => money = value;
    }

    [SerializeField] private int dungeonNum;
    public int DungeonNum
    {
        get { return dungeonNum; }
        set { dungeonNum = value; }
    }
}
