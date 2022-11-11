using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] private ConversationData conversation;
    public void Interaction()
    {
        if(ConversationController.Instance.conversationData == null)
        {
            ConversationController.Instance.conversationData = conversation;
        }


    }
}
