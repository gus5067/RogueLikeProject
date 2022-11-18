using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{

    public float moveSpeed;
    public float colorAlphaSpeed;
    public float destroyTime;

    private TextMeshPro text;
    private Color alpha;

    private void Awake()
    {
        text = GetComponent<TextMeshPro>();
        alpha = text.color;
    }
    private void Start()
    {
        StartCoroutine(destroyRoutine());
    }
    private void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0)); // 텍스트 위치

        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * colorAlphaSpeed); // 텍스트 알파값
        text.color = alpha;
    }
    IEnumerator destroyRoutine()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(this.gameObject);
    }

    public void SetText(int damage)
    {
        text.text = damage.ToString();
    }


}
