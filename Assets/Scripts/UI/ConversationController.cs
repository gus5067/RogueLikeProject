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
    public static ConversationController Instance //static�� ��������� �̱����� �ƴ�
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

    public IEnumerator conversationRoutine()
    {
        Debug.Log("�ڷ�ƾ ����");
        isConversation = true;
        for(int i =0; i < conversationData.Conversations.Length; i++)
        {
            conversationName.text = conversationData.Conversations[i].name;
            text.text = conversationData.Conversations[i].description;
            yield return new WaitforMouseDown();
            yield return new WaitForSeconds(0.1f);
        }
        if (conversationData.nextConversation != null)
        {
            conversationData = conversationData.nextConversation;
            yield return StartCoroutine(conversationRoutine());
        }
        conversationData = null;
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
            return !Input.GetMouseButtonDown(0);
        }
    }

    public WaitforMouseDown()
    {
        Debug.Log("���� ���콺 �Է� �����");
    }
}