using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InLightIcon : MonoBehaviour
{
    [SerializeField] private Sprite inLightIcon;
    [SerializeField] private Sprite notInLightIcon;
    
    private Image img;
    private GameObject player;
    private PlayerAttribute playerAttribute;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerAttribute = player.GetComponent<PlayerAttribute>();
        img = GetComponent<Image>();
        img.sprite = inLightIcon;
    }

    private void Update()
    {
        if(playerAttribute.GetIsInLight()) {
            img.sprite = inLightIcon;
        }
        else
        {
            img.sprite = notInLightIcon;
        }
    }
}
