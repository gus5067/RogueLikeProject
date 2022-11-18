using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServantController : MonoBehaviour
{

    [SerializeField]
    private GameObject[] servants;

    private void Start()
    {
        for (int i = 0; i < ServantManager.Instance.isServantActivate.Length; i++)
        {
            if (ServantManager.Instance.isServantActivate[i])
                servants[i].SetActive(true);
        }
    }
}
