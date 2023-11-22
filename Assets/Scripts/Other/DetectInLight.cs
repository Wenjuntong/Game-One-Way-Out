using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectInLight : MonoBehaviour
{
    //if player hit the collider
    private void OnTriggerStay2D(Collider2D collision)
    {
        //if player is in the light
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
            playerMovement.SetInLight(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //if player is out of the light
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
            playerMovement.SetInLight(false);
        }
    }
}
