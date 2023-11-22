using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Talents/SpeedUp2")]
public class SpeedUp2 : Talents
{
    public override void AffectAttribute()
    {
        PlayerAttribute PlayerAttribute = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttribute>();
        PlayerAttribute.SpeedUp();
        isActivated = true;
    }

    public override void UnLockTalents(Talents newTalent)
    {
        newTalent.isLocked = false;
    }
}
