using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour//�Ŀ� ���׸� �̱������� �����ؾ���
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
