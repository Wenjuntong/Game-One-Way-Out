using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentsHolder : MonoBehaviour
{
    public Talents[] talents;   //healthup1, healthup2
    public GameObject[] talentIcons;

    private GameObject player;
    private PlayerAttribute playerAttribute;
    private PlayerInventory playerInventory;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerAttribute = player.GetComponent<PlayerAttribute>();
        playerInventory = player.GetComponent<PlayerInventory>();

        //set all talents locked
        for (int i = 0; i < talents.Length; i++)
        {
            talents[i].isActivated = false;
            if (talents[i].talentLevel == 0)    talents[i].isLocked = false;
            else
            {
                talents[i].isLocked = true;
            }
        }
    }

    private void Update()
    {
        //set all talents locked
        for (int i = 0; i < talents.Length; i++)
        {
            if (talents[i].isLocked == false)
            {
                talentIcons[i].SetActive(true);
            }
            else {
                talentIcons[i].SetActive(false);
            }
        }
    }

    public void ClickHealthUp1()
    {
        if (!talents[0].isActivated && playerInventory.talentPoint >= talents[0].needTalentPoint)
        {
            talents[0].AffectAttribute();
            talents[0].UnLockTalents(talents[1]);
            playerInventory.TalentPoint -= talents[0].needTalentPoint;
            talentIcons[0].GetComponent<TalentButtons>().ChangeColor();
        }
    }

    public void ClickHealthUp2()
    {
        if (!talents[1].isActivated && playerInventory.TalentPoint >= talents[1].needTalentPoint)
        {
            talents[1].AffectAttribute();
            talents[1].UnLockTalents(talents[2]);
            playerInventory.TalentPoint -= talents[1].needTalentPoint;
            talentIcons[1].GetComponent<TalentButtons>().ChangeColor();
        }
    }

    public void ClickHealthUp3()
    {
        if (!talents[2].isActivated && playerInventory.TalentPoint >= talents[2].needTalentPoint)
        {
            talents[2].AffectAttribute();
            playerInventory.TalentPoint -= talents[2].needTalentPoint;
            talentIcons[2].GetComponent<TalentButtons>().ChangeColor();
        }
        //unlock other talents
        //talents[1].UnLockTalents(talents[1]);
    }

    public void ClickSpeedUp1()
    {
        if (!talents[3].isActivated && playerInventory.TalentPoint >= talents[3].needTalentPoint)
        {
            talents[3].AffectAttribute();
            talents[3].UnLockTalents(talents[4]);
            playerInventory.TalentPoint -= talents[3].needTalentPoint;
            talentIcons[3].GetComponent<TalentButtons>().ChangeColor();
        }
    }

    public void ClickSpeedUp2()
    {
        if (!talents[4].isActivated && playerInventory.TalentPoint >= talents[4].needTalentPoint)
        {
            talents[4].AffectAttribute();
            talents[4].UnLockTalents(talents[5]);
            playerInventory.TalentPoint -= talents[4].needTalentPoint;
            talentIcons[4].GetComponent<TalentButtons>().ChangeColor();
        }
    }

    public void ClickSpeedUp3()
    {
        if (!talents[5].isActivated && playerInventory.TalentPoint >= talents[5].needTalentPoint)
        {
            talents[5].AffectAttribute();
            playerInventory.TalentPoint -= talents[5].needTalentPoint;
            talentIcons[5].GetComponent<TalentButtons>().ChangeColor();
        }
    }

    public void ClickEnduranceUp1()
    {
        if (!talents[6].isActivated && playerInventory.TalentPoint >= talents[6].needTalentPoint)
        {
            talents[6].AffectAttribute();
            talents[6].UnLockTalents(talents[7]);
            playerInventory.TalentPoint -= talents[6].needTalentPoint;
            talentIcons[6].GetComponent<TalentButtons>().ChangeColor();
        }
    }

    public void ClickEnduranceUp2()
    {
        if (!talents[7].isActivated && playerInventory.TalentPoint >= talents[7].needTalentPoint)
        {
            talents[7].AffectAttribute();
            talents[7].UnLockTalents(talents[8]);
            playerInventory.TalentPoint -= talents[7].needTalentPoint;
            talentIcons[7].GetComponent<TalentButtons>().ChangeColor();
        }
    }

    public void ClickEnduranceUp3()
    {
        if (!talents[8].isActivated && playerInventory.TalentPoint >= talents[8].needTalentPoint)
        {
            talents[8].AffectAttribute();
            playerInventory.TalentPoint -= talents[8].needTalentPoint;
            talentIcons[8].GetComponent<TalentButtons>().ChangeColor();
        }
    }

    public void ClickDamageUp1()
    {
        if (!talents[9].isActivated && playerInventory.TalentPoint >= talents[9].needTalentPoint)
        {
            talents[9].AffectAttribute();
            talents[9].UnLockTalents(talents[10]);
            playerInventory.TalentPoint -= talents[9].needTalentPoint;
            talentIcons[9].GetComponent<TalentButtons>().ChangeColor();
        }
    }

    public void ClickDamageUp2()
    {
        if (!talents[10].isActivated && playerInventory.TalentPoint >= talents[10].needTalentPoint)
        {
            talents[10].AffectAttribute();
            talents[10].UnLockTalents(talents[11]);
            playerInventory.TalentPoint -= talents[10].needTalentPoint;
            talentIcons[10].GetComponent<TalentButtons>().ChangeColor();
        }
    }

    public void ClickDamageUp3()
    {
        if (!talents[11].isActivated && playerInventory.TalentPoint >= talents[11].needTalentPoint)
        {
            talents[11].AffectAttribute();
            playerInventory.TalentPoint -= talents[11].needTalentPoint;
            talentIcons[11].GetComponent<TalentButtons>().ChangeColor();
        }
    }
}
