using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    //public Animator animator;
    public float gravity = -9.8f;
    private Vector3 playerVelocity;
    private bool isCharacterMoving;
    public float speed = 10f;
    private int jumpCount = 0;
    private float jumpForce = 5.0f;
    public Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3 (horizontal, 0f, vertical).normalized;

        isCharacterMoving = direction.magnitude >= 0.1f;

        //Controller
        if(isCharacterMoving)
        {
            float targetAgle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            Vector3 moveDir = Quaternion.Euler(0f, targetAgle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        //Jump
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce);
            jumpCount += 1;
        }

        if(jumpCount == 1)
        {
            jumpForce = 0f;
        }
        
        //Gravity
        if (controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
            jumpCount = 0;
        }
        else
        {
            playerVelocity.y += gravity * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);
        }
    }
}
