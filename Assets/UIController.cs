using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] Player player;

    private void Awake()
    {
        player.onPlayerDie += PlayerDie;
    }

    public void PlayerDie()
    {
        gameObject.SetActive(false);
    }
}
