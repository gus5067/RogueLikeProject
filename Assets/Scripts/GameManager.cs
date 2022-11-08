using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour//후에 제네릭 싱글톤으로 변경해야함
{

    public static GameManager instance;

    [SerializeField] private int money;
    public int Money
    {
        get => money;
        set => money = value;
    }

    private void Awake()
    {
        instance = this;
    }

    


}
