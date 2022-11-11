using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Conversation
{
    public string name;
    [TextArea(1, 10)]
    public string description;
}


[CreateAssetMenu(menuName ="Conversation/ConversationData")]
public class ConversationData : ScriptableObject
{
    [SerializeField] private Conversation[] conversations;

    public Conversation[] Conversations
    {
        get { return conversations; }
    }

    public ConversationData nextConversation;
}
