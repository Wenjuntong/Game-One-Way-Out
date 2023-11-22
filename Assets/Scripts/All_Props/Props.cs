using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Props: ScriptableObject
{
    public new string name;
    public float cooldownTime;
    public float activeTime;
    public float preparationTime;
    public string keyCode;

    public virtual void Ready(GameObject parent) { }
    public virtual void Activate(GameObject parent) { }

    public virtual void CoolDown(GameObject parent) { }

    public virtual void Clear() { }

}
