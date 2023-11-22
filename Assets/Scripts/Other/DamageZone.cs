using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{

    private string targetTag = "Player";

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            PlayerAttribute controller = other.GetComponent<PlayerAttribute>();
            if (controller != null)
            {
                controller.TrapDamage(10f);
            }
        }

    }

}
