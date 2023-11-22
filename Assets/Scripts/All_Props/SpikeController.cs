using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    private string targetTag = "Player";
    public Animator animator;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            animator.SetTrigger("PlayAnimation");
            collision.GetComponent<PlayerAttribute>().TrapDamage(10);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            animator.SetTrigger("StopAnimation");
        }
    }
}
