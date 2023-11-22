using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MoneyPickUp : MonoBehaviour
{
    private int coinMoney;

    // Start is called before the first frame update
    void Start()
    {
        coinMoney = Random.Range(1, 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            IInventory inventory = collision.gameObject.GetComponent<IInventory>();

            if(inventory != null)
            {
                inventory.Money += coinMoney;
                Destroy(gameObject);
            }
        }
    }
}
