using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if hit player
        if (collision.gameObject.tag == "Player")
        {
            //get player health
            PlayerAttribute playerAttribute = collision.gameObject.GetComponent<PlayerAttribute>();
            //if player health is not null
            if (playerAttribute != null)
            {
                //take damage
                playerAttribute.TakeDamage(30);
            }
        }
    }
}
