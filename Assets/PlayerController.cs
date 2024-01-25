using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //make and set player input variables
    PlayerInput playerInput;
    InputAction player_1_move;
    InputAction player_1_jump;
    InputAction player_1_interact;
    InputAction player_2_move;
    InputAction player_2_jump;
    InputAction player_2_interact;

    //set player index
    public enum players { player_1,  player_2 };
    [Header("Set Player Index")]
    public players PlayerIndex;

    //set general settings for character controller
    [Header("General Player Controller Settings")]
    public CharacterController controller;
    public float gravity = -9.8f;
    public float playerSPEED = 10f;
    public float playerJUMPFORCE = 6.5f;
    public Animator animator;


    //get the transform of additional components
    [Header("Set Components Transform")]
    public Transform camTransfrom;
    public Transform skinTransform;

    //general variable declaration
    private int jumpCount = 0;
    private Vector3 velocity;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        player_1_move = playerInput.actions.FindAction("player_1_move");
        player_1_jump = playerInput.actions.FindAction("player_1_jump");
        player_1_interact = playerInput.actions.FindAction("player_1_interact");
        player_2_move = playerInput.actions.FindAction("player_2_move");
        player_2_jump = playerInput.actions.FindAction("player_2_jump");
        player_2_interact = playerInput.actions.FindAction("player_2_interact");
    }

    void Update()
    {
        Vector2 inputMap = player_1_move.ReadValue<Vector2>();
        Vector3 direction = new Vector3(inputMap.x, 0, inputMap.y);
        direction = Quaternion.AngleAxis(camTransfrom.eulerAngles.y, Vector3.up) * direction;

        //Gravity
        if (!controller.isGrounded) { 
            velocity.y += gravity * Time.deltaTime;
            animator.SetBool("isJumping", true);
        }else
        {
            jumpCount = 0;
            animator.SetBool("isJumping", false);
        }

        if (player_1_jump.WasPerformedThisFrame() && jumpCount < 2)
        {
            velocity.y = playerJUMPFORCE;
            jumpCount += 1;
        }

        if (direction.magnitude > 0.01f)
        {
            Vector3 skinRotation = skinTransform.eulerAngles;
            skinRotation.y = Mathf.LerpAngle(skinRotation.y, Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg, playerSPEED * 1.5f * Time.deltaTime);
            skinTransform.eulerAngles = skinRotation;
            velocity.x = direction.x * playerSPEED;
            velocity.z = direction.z * playerSPEED;
            animator.SetBool("isMoving", true);
        }else
        {
            velocity.x = Mathf.MoveTowards(velocity.x, 0, playerSPEED);
            velocity.z = Mathf.MoveTowards(velocity.z, 0, playerSPEED);
            animator.SetBool("isMoving", false);
        }

        if (player_1_interact.WasPressedThisFrame())
        {
            
        }

        controller.Move(velocity * Time.deltaTime);
    }
}
