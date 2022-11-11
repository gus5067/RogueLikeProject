using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Conversation/QuestData")]
public class QuestData : ScriptableObject
{
    [SerializeField] private Conversation[] questConversations;

    public Conversation[] QuestConversations
    {
        get { return questConversations; }
    }

    public int reward;
}
