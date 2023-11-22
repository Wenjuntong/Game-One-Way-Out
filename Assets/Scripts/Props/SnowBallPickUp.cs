using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBallPickUp : MonoBehaviour
{
    private int addValue = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IInventory inventory = collision.gameObject.GetComponent<IInventory>();
            PlayerInventory playerInventory = collision.gameObject.GetComponent<PlayerInventory>();

            if (inventory != null)
            {
                inventory.SnowBallNum += addValue;
                playerInventory.EquipSnowBall();
                Destroy(gameObject);
            }
        }
    }
}
