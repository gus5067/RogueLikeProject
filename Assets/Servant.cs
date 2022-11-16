using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Servant : MonoBehaviour
{
    private Queue<Vector3> playerPos = new Queue<Vector3>();

    [SerializeField]
    private Transform player;
    float initScaleX;
    private Vector3 followPos;

    [SerializeField] private Animator anim;
    public int delayCount;

    private void Start()
    {
        initScaleX = transform.localScale.x;
        transform.position = player.position + Vector3.up;
    }

    private void Update()
    {
        nextPosition();
        Follow();
    }
    public void nextPosition()
    {
        if (!playerPos.Contains(player.position))
            playerPos.Enqueue(player.position);



        if (playerPos.Count > delayCount)
        {
            followPos = playerPos.Dequeue();
            anim.SetBool("Run", true);
        }
        else
            anim.SetBool("Run", false);



    }
    public void Follow()
    {
        transform.position = followPos;

        if ((player.position.x - transform.position.x) < 0)
            transform.localScale = new Vector3(initScaleX, transform.localScale.y, transform.localScale.z);
        else if (player.position.x - transform.position.x > 0)
            transform.localScale = new Vector3(-initScaleX, transform.localScale.y, transform.localScale.z);



    }

}
