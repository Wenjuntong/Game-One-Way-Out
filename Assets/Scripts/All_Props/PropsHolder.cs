using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropsHolder : MonoBehaviour
{
    public Props[] props;
    public float activeTime;
    public float preparationTime;
    public float cooldownTime;

    private Props currentProp;
    private PlayerInventory playerInventory;
    private PlayerAttribute playerAttribute;

    enum AbilityState
    {
        ready,
        active,
        preparation,
        cooldown
    }
    AbilityState state = AbilityState.ready;

    private void Start()
    {
        playerInventory = gameObject.GetComponent<PlayerInventory>();
        playerAttribute = gameObject.GetComponent<PlayerAttribute>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state) { 
            case AbilityState.ready:
                if (Input.GetKeyDown(props[0].keyCode))
                {
                    if (playerInventory.SnowBallNum > 0 && currentProp == null && !playerAttribute.isDead)
                    {
                        //Activate
                        currentProp = props[0];
                        props[0].Ready(gameObject);
                        state = AbilityState.active;
                        activeTime = props[0].activeTime;
                    }
                    else
                    {
                        currentProp = null;
                    }
                }
                break;
            case AbilityState.active:
                if (activeTime <= 0)
                {
                    //Deactivate
                    state = AbilityState.preparation;
                    preparationTime = currentProp.preparationTime;
                }
                else
                {
                    if (Input.GetMouseButtonDown(0) && GetComponent<PlayerMovement>().equipedObject.CompareTag("Snowball"))
                    {
                        currentProp.Activate(gameObject);
                        activeTime = 0;
                    }
                    activeTime -= Time.deltaTime;
                }
                break;
            case AbilityState.preparation:
                if(preparationTime <= 0)
                {
                    state = AbilityState.cooldown;
                    currentProp.CoolDown(gameObject);
                    cooldownTime = currentProp.cooldownTime;
                    currentProp.Clear();
                    currentProp = null;
                }
                else
                {
                    preparationTime -= Time.deltaTime;
                }
                break;
            case AbilityState.cooldown:
                if (cooldownTime <= 0)
                {
                    //Ready
                    state = AbilityState.ready;
                }
                else
                {
                    cooldownTime -= Time.deltaTime;
                }
                break;
        }
    }
}
