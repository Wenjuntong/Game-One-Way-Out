using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLight : MonoBehaviour
{
    // 用于标识碰撞对象的Tag
    public string triggerTag;
    private bool isOnFire;

    // 用于存储子类对象
    public GameObject childObject;

    private void Start()
    {
        isOnFire = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 如果碰撞对象的Tag与指定的Tag匹配，则激活子类
        if (other.CompareTag(triggerTag))
        {
            childObject.SetActive(true);
            isOnFire |= true;
        }
    }

    public bool GetIsOnFire()
    {
        return isOnFire;
    }
}
