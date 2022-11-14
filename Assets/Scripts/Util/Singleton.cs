using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                T manager = FindObjectOfType<T>();
                if (manager != null)
                {
                    instance = manager;
                }
                else
                {
                    instance = new GameObject("Manager").AddComponent<T>();
                }
            }
            return instance;
        }
        set { instance = value; }
    }


    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}
