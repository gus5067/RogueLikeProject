using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlinkSprite : MonoBehaviour
{
    public Color normalColor;

    public Color blinkColor;

    private Coroutine corutine;

    private SpriteRenderer spriteRenderer;

    [SerializeField] Transform playerPos;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        corutine = StartCoroutine(BlinkRoutine());
    }
    public void OnDisable()
    {
        StopCoroutine(corutine);
    }

    public void LateUpdate()
    {
        transform.position = playerPos.position + Vector3.up * 2.5f;
    }
    IEnumerator BlinkRoutine()
    {
        while(true)
        {
            spriteRenderer.color = new Color(blinkColor.r, blinkColor.g, blinkColor.b, blinkColor.a);
            yield return new WaitForSeconds(1f);
            spriteRenderer.color = new Color(normalColor.r, normalColor.g, normalColor.b, normalColor.a);
            yield return new WaitForSeconds(1f);
        }
    }
}
