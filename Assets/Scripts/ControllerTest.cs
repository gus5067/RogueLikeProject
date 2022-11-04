using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum PlayerMoveType
{
    TopDown, Platform
}
[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class ControllerTest : MonoBehaviour
{

    [SerializeField] Animator anim;
    Rigidbody2D rb;

    [SerializeField] private PlayerMoveType moveType;

    [SerializeField, Range(1f, 10f)]
    private float speed;
    public float Speed
    {
        get { return speed; }
        set
        {
            if (value > maxSpeed)
                speed = maxSpeed;
            else
                speed = value;
        }
    }

        [SerializeField] private float maxSpeed;

    [SerializeField, Range(1f, 10f)]
    private float accelSpeed;

    float inputX;
    float inputY;
    float initScaleX;

    [SerializeField] RectTransform hitPoint;

    public event UnityAction<int> onChangeWeapon;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        initScaleX = transform.localScale.x;
    }
    private void Update()
    {
        Move();
        switch (moveType)
        {
            case PlayerMoveType.TopDown:
              
                HitPointMove();

                if (Input.GetKeyDown(KeyCode.K))
                {
                    anim.SetTrigger("AxeAttack");
                    onChangeWeapon?.Invoke(0);
                }
                else if (Input.GetKeyDown(KeyCode.J))
                {
                    anim.SetTrigger("Attack");
                    onChangeWeapon?.Invoke(1);
                }
                break;
            case PlayerMoveType.Platform:
                break;

        }

    }
    public void Move()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");

        if (inputX > 0)
        {
            transform.localScale = new Vector3(-1f * initScaleX, transform.localScale.y, transform.localScale.z);
        }
        else if (inputX < 0)
        {
            transform.localScale = new Vector3(initScaleX, transform.localScale.y, transform.localScale.z);
        }
        if (inputX != 0 || inputY != 0)
        {
            anim.SetBool("Run", true);
        }
        else
        {
            anim.SetBool("Run", false);
        }

        switch(moveType)
        {
            case PlayerMoveType.Platform:
                if(Mathf.Abs(inputX) > 0)
                {
                    Speed += accelSpeed;
             
                }
                else
                {
                    Speed -= accelSpeed;
                }
                rb.velocity = new Vector2(Speed * inputX, rb.velocity.y);
                break;
            case PlayerMoveType.TopDown:
                transform.Translate(new Vector2(Mathf.RoundToInt(inputX), Mathf.RoundToInt(inputY)) * Time.deltaTime * Speed);
                break;
        }
       
    }
    public void HitPointMove()
    {
        if(Mathf.Abs(inputX) > 0 || Mathf.Abs(inputY) > 0)
        {
            hitPoint.anchoredPosition = new Vector2(-0.5f * Mathf.Abs(inputX), 0.3f + inputY * 0.6f);
        }
        else
        {
            hitPoint.anchoredPosition = new Vector2(-0.5f, 0.3f);
        }
    }
}
