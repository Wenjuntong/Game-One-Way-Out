using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talents : ScriptableObject
{
    public string talentName;
    public int talentLevel;
    public int needTalentPoint;
    public bool isLocked;
    public bool isActivated;
    public virtual void AffectAttribute() { }
    public virtual void UnLockTalents(Talents newTalent) {}
}
