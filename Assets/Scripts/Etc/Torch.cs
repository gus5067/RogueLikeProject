using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    private Player player;

    [SerializeField, Range(0, 15f)] private float torchRange;
    public float TorchRange
    {
        get
        {
            return torchRange;
        }
        set
        {
            torchRange = value;
            transform.localScale = new Vector3(TorchRange, TorchRange, 1);
        }
    }

    private void Awake()
    {
        player = transform.parent.GetComponent<Player>();
        player.onPlayerDie += OnPlayerDie;
    }

    private void Start()
    {
        TorchRange = GameManager.Instance.torchRange;
    }
    public void OnPlayerDie()
    {
        StartCoroutine(torchRoutine());
    }

    IEnumerator torchRoutine()
    {
        WaitForSecondsRealtime time = new WaitForSecondsRealtime(0.02f);
        while(TorchRange < 15f)
        {
            TorchRange += 0.2f;
            yield return time;
        }
    }


}
