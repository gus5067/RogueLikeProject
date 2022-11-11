using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConversationController : MonoBehaviour
{

    [SerializeField] private GameObject conversationUI;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI conversationName;

    public int conversationNum = 0;
    public ConversationData conversationData;
    public QuestData questData;

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
        conversationName.text = conversationData.Conversations[0].name;
        text.text = conversationData.Conversations[0].description;
    }

    IEnumerator conversationRoutine()
    {
        yield return null;
    }

}
