using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Player : MonoBehaviour, IDamageable, IForceable
{
    [SerializeField]
    private GameObject interactUI;
    [SerializeField]
    private GameObject curTarget;

    public GameObject CurTarget
    {
        get => curTarget;
        set
        {
            curTarget = value;
            if(value == null)
                interactUI.SetActive(false);
            else
                interactUI.SetActive(true);
        }
    }
    [SerializeField] private int hp;
    public int Hp
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
            GameManager.Instance.playerHp = hp;
            if(hp <= 0)
            {
                hp = 0;
                Die();
            }
        }
    }

    private Rigidbody2D rb;

    private SpriteRenderer[] renderers;

    Color color1 = new Color(1, 1, 1, 0);
    Color color2 = new Color(1, 1, 1, 1);

    Color[] colors;

    public event UnityAction onPlayerDie;

    [SerializeField] private GameObject ghost;

    [SerializeField]
    private bool isHit = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Hp = GameManager.Instance.playerHp;
        renderers = GetComponentsInChildren<SpriteRenderer>();
        colors = new Color[renderers.Length];

        for(int i=0; i < renderers.Length;i++)
            colors[i] = renderers[i].color;
    }
    private void FixedUpdate()
    {
        InteractWithTarget();
    }
    public void HitDamage(int damage)
    {
        if (!isHit)
            Hp -= damage;
        else
            return;

        StartCoroutine(HitTime(1));
        StartCoroutine(Blink());
    }
    public void TakeForce(Vector2 dir, int power)
    {
        if (!isHit)
            rb.AddForce(dir * power, ForceMode2D.Impulse);
    }

    /// <summary>
    /// 플레이어의 피격 판정 시간
    /// </summary>
    /// <param name="hitTime"></param>
    /// <returns></returns>
    IEnumerator HitTime(float hitTime)
    {
        isHit = true;
        yield return new WaitForSeconds(hitTime);
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

    [SerializeField] float dieTime;
    public void Die()
    {
        if (ghost != null)
            ghost.SetActive(true);
        onPlayerDie?.Invoke();
        StartCoroutine(PlayerDie(dieTime));
    }

    IEnumerator PlayerDie(float time)
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(time);
        Time.timeScale = 1;
        GameManager.Instance.Money -= 50;
        if(GameManager.Instance.Money < 0)
        {
            LoadManager.LoadScene("MiningCaveScene");
        }
        else
            LoadManager.LoadScene("TownScene");

    }

    public void InteractWithTarget()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            if (CurTarget == null)
                return;
            IInteractable interactTarget = CurTarget.GetComponent<IInteractable>();
            interactTarget?.Interaction();
        }
    }
}
