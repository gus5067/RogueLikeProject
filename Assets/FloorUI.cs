using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloorUI : MonoBehaviour
{

    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>(); 
    }

    private void Start()
    {
        text.text = "-" + GameManager.Instance.DungeonNum.ToString() + "Ãþ-";
        StartCoroutine(TextRoutine());
    }

    IEnumerator TextRoutine()
    {
        float temp = text.color.a;
       while(text.color.a >0)
        {
            text.color = new Color(1f, 1f, 1f, temp);
            yield return new WaitForSeconds(0.01f);
            temp -= 0.01f;
        }
        gameObject.SetActive(false);
    }
}
