using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DialogueEditor;
using UnityEngine.InputSystem;


public class ConversationStarter : MonoBehaviour
{
    PlayerInput playerInput;
    InputAction player_1_interact;
    InputAction player_2_interact;

    [SerializeField] private NPCConversation myConversation;

    // Define UnityEvents for different stages of the conversation
    public UnityEvent onPlayerEnterTrigger; // Event when the player enters the trigger
    public UnityEvent onPlayerExitTrigger;
    public UnityEvent onConversationStart; // Event when the conversation starts
    public UnityEvent onConversationEnd; // Event when the conversation ends

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        player_1_interact = playerInput.actions.FindAction("player_1_interact");
        player_2_interact = playerInput.actions.FindAction("player_2_interact");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Invoke the event when the player enters the trigger
            onPlayerEnterTrigger.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            onPlayerExitTrigger.Invoke();
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E) || other.CompareTag("Player") && Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("pencet");
            // Invoke the start event and start the conversation
            onConversationStart.Invoke();
            ConversationManager.Instance.StartConversation(myConversation);
            ConversationManager.OnConversationEnded -= OnConversationEnded;
            ConversationManager.OnConversationEnded += OnConversationEnded;
        }
    }

    private void OnConversationEnded()
    {
        // Invoke the end event
        onConversationEnd.Invoke();
        ConversationManager.OnConversationEnded -= OnConversationEnded;
    }
}
