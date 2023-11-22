using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Talents/DamageUp1")]
public class DamageUp1 : Talents
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
