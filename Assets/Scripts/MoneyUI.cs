using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] TextMeshProUGUI text;

    private void Start()
    {
        GameManager.Instance.OnChangeMoney += MoneyChange;
        text.text = GameManager.Instance.Money.ToString();
    }
    public void MoneyChange(int money)
    {
        anim.SetTrigger("Flip");
        text.text = money.ToString();
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnChangeMoney -= MoneyChange;
    }
}
