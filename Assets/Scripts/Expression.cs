using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Expression : MonoBehaviour
{
    public Animator animator;

    public void Laughing()
    {
        animator.SetBool("isLaughing", true);
    }

    public void Idle ()
    {
        animator.SetBool("isLaughing", false);
    }
}
