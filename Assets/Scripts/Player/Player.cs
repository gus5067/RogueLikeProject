using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum PlayerState
{
    Normal, Frozen, Burn
}
public class Player : MonoBehaviour, IDamageable
{
    [SerializeField]
    private PlayerState playerState;
    public PlayerState PlayerState
    { 
        get => playerState;
        set
        {
            if (isNormalState)
            {
                playerState = value;
                ChangeState(value);
            }
               
        }
    }
    public bool isNormalState = true;
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
            GameManager.Instance.PlayerHp = hp;
            if(hp <= 0)
            {
                hp = 0;
                Die();
            }
        }
    }

    private Rigidbody2D rb;

    private SpriteRenderer[] renderers;

    Color[] colors;

    public event UnityAction onPlayerDie;

    [SerializeField] private GameObject frozenVfx;
    [SerializeField] private GameObject burnVfx;
    [SerializeField] private GameObject ghost;

    [SerializeField]
    private bool isHit = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Hp = GameManager.Instance.PlayerHp;
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

    public void ChangeState(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.Frozen:
                StartCoroutine(FrozenState());
                break;
            case PlayerState.Burn:
                StartCoroutine(BurnState());
                break;
        }
    }

    IEnumerator FrozenState()
    {
        isNormalState = false;
        float temp = gameObject.GetComponent<ControllerTest>().Speed;
        frozenVfx.SetActive(true);
        gameObject.GetComponent<ControllerTest>().Speed = 0f;
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<ControllerTest>().Speed = temp;
        isNormalState = true;
        PlayerState = PlayerState.Normal;
        frozenVfx.SetActive(false);
    }

    IEnumerator BurnState()
    {
        isNormalState = false;
        burnVfx.SetActive(true);
        for (int i = 0; i < 3; i++)
        {
            this.HitDamage(GameManager.Instance.PlayerHp / 5);
            yield return new WaitForSeconds(0.2f);
        }
        isNormalState = true;
        burnVfx.SetActive(false);
        PlayerState = PlayerState.Normal;

    }

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
        burnVfx.SetActive(false);
        frozenVfx.SetActive(false);
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
        GameManager.Instance.Money -= 100;
        if(GameManager.Instance.Money < -1000)
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
