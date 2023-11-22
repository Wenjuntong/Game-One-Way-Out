using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            PlayerInventory playerInventory = collision.gameObject.GetComponent<PlayerInventory>();

            playerInventory.UpdateWeapon(gameObject);
            Destroy(gameObject);
            
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                PlayerInventory playerInventory = collision.gameObject.GetComponent<PlayerInventory>();

                playerInventory.UpdateWeapon(gameObject);
                Destroy(gameObject);
            }
        }
    }
}
