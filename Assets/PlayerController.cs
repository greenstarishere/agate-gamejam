using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    //public Animator animator;
    public float gravity = -9.8f;
    private Vector3 velocity;
    private bool isCharacterMoving;
    public float playerSPEED = 10f;
    private int jumpCount = 0;
    public float playerJUMPFORCE = 6.5f;
    public GameObject skin;
    public Camera camera;

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3 (horizontal, 0f, vertical).normalized;
        direction = Quaternion.AngleAxis(camera.transform.rotation.y, Vector3.up);
        //Gravity
        if (!controller.isGrounded) { 
            velocity.y += gravity * Time.deltaTime;
        }else
        {
            jumpCount = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
        {
            velocity.y = playerJUMPFORCE;
            jumpCount += 1;
        }

        if (direction.magnitude > 0.01f)
        {
            velocity.x = direction.x * playerSPEED;
            velocity.z = direction.z * playerSPEED;
        }else
        {
            velocity.x = Mathf.MoveTowards(velocity.x, 0, playerSPEED);
            velocity.z = Mathf.MoveTowards(velocity.z, 0, playerSPEED);
        }

        controller.Move(velocity * Time.deltaTime);
    }
}
