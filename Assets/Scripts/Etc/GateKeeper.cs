using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GateKeeper : NPC
{
    [SerializeField]
    private ConversationData specialConversation;

    private void Awake()
    {
        GameManager.Instance.OnMoneyChange += CheckPlayerMoney;
    }


    public void CheckPlayerMoney(int money)
    {
        if (money >= 0)
            StartCoroutine(EndMiningRoutine()); 
        else
            return;
    }

    IEnumerator EndMiningRoutine()
    {
        Debug.Log("후에 대화 구문 구현");
        //ConversationController.Instance.conversationData = specialConversation;
       //yield return ConversationController.Instance.conversationRoutine();
        yield return new WaitForSeconds(0.05f);
        LoadManager.LoadScene("TownScene");
    }
}

