using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DialogueEditor;
using UnityEngine.InputSystem;
using TMPro;


public class ConversationStarter : MonoBehaviour
{
    public PlayerInput playerInput;
    InputAction player_1_interact;
    InputAction player_2_interact;

    [SerializeField] private NPCConversation myConversation;
    [SerializeField] private TextMeshProUGUI playerText; 

    // Define UnityEvents for different stages of the conversation
    public UnityEvent onPlayerEnterTrigger; // Event when the player enters the trigger
    public UnityEvent onPlayerExitTrigger;
    public UnityEvent onConversationStart; // Event when the conversation starts
    public UnityEvent onConversationEnd; // Event when the conversation ends

    private void Start()
    {
        player_1_interact = playerInput.actions.FindAction("player_1_interact");
        player_2_interact = playerInput.actions.FindAction("player_2_interact");
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null)
        {
            if (playerController.PlayerIndex == PlayerController.players.player_1)
            {
                playerText.text = "Press (E) to Interact";
                onPlayerEnterTrigger.Invoke();
            }
            else if (playerController.PlayerIndex == PlayerController.players.player_2)
            {
                playerText.text = "Press (O) to Interact";
                onPlayerEnterTrigger.Invoke();
            }
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
        if (player_1_interact.WasPerformedThisFrame() || player_2_interact.WasPerformedThisFrame())
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