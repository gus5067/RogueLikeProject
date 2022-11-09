using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour//후에 제네릭 싱글톤으로 변경해야함
{

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if(instance == null)
            {
                GameManager manager = FindObjectOfType<GameManager>();
                if(manager != null)
                {
                    instance = manager;
                }
                else
                {
                    instance = new GameManager();
                }
            }
            return instance;
        }
        set { instance = value; }
    }

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

    private void Awake()
    {
        if(Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    


}
