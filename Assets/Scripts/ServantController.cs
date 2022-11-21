using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServantController : MonoBehaviour
{

    [SerializeField]
    private GameObject[] servants;

    private void Start()
    {
        if (EquipManager.Instance.curServantData != null)
            servants[EquipManager.Instance.curServantData.servantNum].SetActive(true);
    }
}
