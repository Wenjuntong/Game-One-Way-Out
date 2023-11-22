using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Environment") || other.CompareTag("Player"))
        {
            Destroy(gameObject);

            //if hit the player, player has to take damage
            if (other.CompareTag("Player"))
            {
                other.GetComponent<PlayerAttribute>().TakeDamage(6);
            }
        }
    }
}
