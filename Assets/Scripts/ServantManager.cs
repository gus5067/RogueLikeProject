using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServantManager : Singleton<ServantManager>
{

    public bool[] isServantActivate;

    private new void Awake()
    {
        base.Awake();
        SceneManager.sceneLoaded += ClearManager;
    }


    public void ClearManager(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "MiningCaveScene")
            return;

        for(int i = 0; i< isServantActivate.Length; i++)
        {
            isServantActivate[i] = false;
        }
    }
}

