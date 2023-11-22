using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Props/SnowBall")]
public class SnowBallProps : Props
{
    [SerializeField] private GameObject sandBallPrefab;
    [SerializeField] private ParticleSystem iceZoneEffect;

    private ParticleSystem skillEffect;

    public override void Ready(GameObject parent) {
        PlayerMovement movement = parent.GetComponent<PlayerMovement>();
        movement.SetEquipped(parent.transform.GetChild(2).GetChild(0).gameObject);
    }

    public override void Activate(GameObject parent)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 iceZonePosition = new Vector2(mousePosition.x, mousePosition.y);
        skillEffect = Instantiate(iceZoneEffect, iceZonePosition, Quaternion.identity);
        skillEffect.Play();

        PlayerMovement movement = parent.GetComponent<PlayerMovement>();
        PlayerInventory inventory = parent.GetComponent<PlayerInventory>();
        inventory.SnowBallNum -= 1;
        Destroy(movement.equipedObject);
    }

    public override void CoolDown(GameObject parent)
    {
        PlayerMovement movement = parent.GetComponent<PlayerMovement>();
        movement.ResetEquipped();
    }

    public override void Clear()
    {
        if(skillEffect != null)
        {
            Destroy(skillEffect.gameObject);
            skillEffect = null;
        }
    }
}
