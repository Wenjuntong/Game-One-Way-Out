using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Talents/DamageUp3")]
public class DamageUp3 : Talents
{
    public override void AffectAttribute()
    {
        PlayerAttribute PlayerAttribute = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttribute>();
        PlayerAttribute.DamageUp();
        isActivated = true;
    }

    public override void UnLockTalents(Talents newTalent)
    {
        newTalent.isLocked = false;
    }
}
