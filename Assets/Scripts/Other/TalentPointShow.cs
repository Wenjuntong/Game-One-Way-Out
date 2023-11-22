using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalentPointShow : MonoBehaviour
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
        if (playerInventory != null)
        {
            gameObject.GetComponent<Text>().text = playerInventory.TalentPoint.ToString();
        }
    }
}
