using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour,IDamageable,IForceable
{
    [SerializeField] private int hp;
    public int Hp { get { return hp; } set { hp = value; } }

    private Rigidbody2D rb;

    private SpriteRenderer[] renderers;

    Color color1 = new Color(1, 1, 1, 0);
    Color color2 = new Color(1, 1, 1, 1);

    Color[] colors;
    [SerializeField]
    private bool isHit = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        renderers = GetComponentsInChildren<SpriteRenderer>();
        colors = new Color[renderers.Length];

        for(int i=0; i < renderers.Length;i++)
            colors[i] = renderers[i].color;
    }

    public void HitDamage(int damage)
    {
        if(!isHit)
        {
            Hp -= damage;
        }
        
        StartCoroutine(HitTime());
        StartCoroutine(Blink());
    }

    public void TakeForce(Vector2 dir, int power)
    {
        if(!isHit)
        {
            rb.AddForce(dir * power, ForceMode2D.Impulse);
        } 
    }

    IEnumerator HitTime()
    {
        isHit = true;
        yield return new WaitForSeconds(1f);
        isHit = false;
    }

    IEnumerator Blink()
    {
        WaitForSeconds waitTime = new WaitForSeconds(0.1f);
        bool isAlpha = false;
        Color tempCol;
        while (isHit)
        {
            isAlpha = !isAlpha;   
            for (int i=0;i<colors.Length;i++)
            {
                tempCol = colors[i];
                tempCol.a = isAlpha ? 0 : colors[i].a;
                renderers[i].color = tempCol;
            }
            yield return waitTime;
        }
        for (int i = 0; i < colors.Length; i++)
        {
            renderers[i].color = colors[i];
        }


    }
}
