using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public float gravity = -9.8f;
    private Vector3 velocity;
    public Animator animator;
    public float playerSPEED = 10f;
    private int jumpCount = 0;
    public float playerJUMPFORCE = 6.5f;
    public Transform camTransfrom;
    public Transform skinTransform;

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3 (horizontal, 0f, vertical).normalized;
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

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
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

        controller.Move(velocity * Time.deltaTime);
    }
}
