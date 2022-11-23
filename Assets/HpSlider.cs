using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpSlider : MonoBehaviour
{

    private Slider slider;

    private void Awake()
    {
        GameManager.Instance.OnChangeHp += SetHp;
        slider = GetComponent<Slider>();
    }

    private void Start()
    {
        slider.value = GameManager.Instance.PlayerHp / GameManager.Instance.PlayerMaxHp;
    }
    public void SetHp(int hp)
    {
        slider.value = (float)hp / (float)GameManager.Instance.PlayerMaxHp;
    }
}
