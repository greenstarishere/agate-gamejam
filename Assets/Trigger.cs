using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class Trigger : MonoBehaviour
{
    [SerializeField] private NPCConversation myConversation;
    private bool conversationStarted = false;

    // Start is called before the first frame update
    void Update()
    {
        if (Input.anyKey && !conversationStarted)
        {
            conversationStarted = true;
            ConversationManager.Instance.StartConversation(myConversation);
        }
    }
}
