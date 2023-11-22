using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Talents/SpeedUp1")]
public class SpeedUp1 : Talents
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
