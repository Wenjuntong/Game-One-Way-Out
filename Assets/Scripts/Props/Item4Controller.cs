using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item4Controller : MonoBehaviour
{
    private GameObject player;
    private PlayerInventory playerInventory;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerInventory = player.GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if(playerInventory.BombNum > 0)
            {
                transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
}
