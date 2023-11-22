using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item1Controller : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if(player.transform.GetChild(0) != null)
            {
                if (player.transform.GetChild(0).GetChild(0).CompareTag("Pistol"))
                {
                    GetComponent<Image>().sprite = sprites[0];
                }
                else if (player.transform.GetChild(0).GetChild(0).CompareTag("Shotgun"))
                {
                    GetComponent<Image>().sprite = sprites[1];
                }
                else
                {
                    GetComponent<Image>().sprite = sprites[2];
                }
            }
        }
    }
}
