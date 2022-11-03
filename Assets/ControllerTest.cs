using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class ControllerTest : MonoBehaviour
{

    [SerializeField] Animator anim;

    [SerializeField, Range(1f, 10f)]
    private float speed;
    public float Speed { get { return speed; }}

    [SerializeField]
    float inputX;
    [SerializeField]
    float inputY;

    float initScaleX;

    private void Start()
    {
        initScaleX = transform.localScale.x;
    }
    private void Update()
    {
        CharcterMoveTest();
    }
    public void CharcterMoveTest()
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

        transform.Translate(new Vector2(Mathf.RoundToInt(inputX), Mathf.RoundToInt(inputY)) * Time.deltaTime * Speed);

    }

}
