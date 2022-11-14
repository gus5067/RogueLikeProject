using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>//후에 제네릭 싱글톤으로 변경해야함
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
