using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConversationController : MonoBehaviour
{

    [SerializeField] private GameObject conversationUI;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI conversationName;

    public bool isConversation;
    public ConversationData conversationData;

    private static ConversationController instance;
    public static ConversationController Instance
    {
        get
        {
            return instance;
        }
        set { instance = value; }
    }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void ConversationProgress()
    {
        conversationUI.SetActive(true);
        if(isConversation == false)
        {
            StartCoroutine(conversationRoutine());
        }
       
    }

    IEnumerator conversationRoutine()
    {
        isConversation = true;
        for(int i =0; i < conversationData.Conversations.Length; i++)
        {
            conversationName.text = conversationData.Conversations[i].name;
            text.text = conversationData.Conversations[i].description;
            Debug.Log("마우스 입력 전 : " + i);
            yield return new WaitforMouseDown();
            Debug.Log("마우스 입력 후 : " + i);
        }
        yield return new WaitforMouseDown();
        conversationUI.SetActive(false);
        isConversation = false;
    }

}

public class WaitforMouseDown : CustomYieldInstruction
{
    public override bool keepWaiting
    {
        get
        {
            return !Input.GetKeyDown(KeyCode.M);
        }
    }

    public WaitforMouseDown()
    {
        Debug.Log("왼쪽 마우스 입력 대기중");
    }
}