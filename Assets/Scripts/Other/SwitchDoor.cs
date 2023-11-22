using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchDoor : MonoBehaviour
{
    // 要切换到的另一个Sprite
    public GameObject otherSprite;

    // 在两个Sprite之间切换的方法
    public void SwitchToOtherSprite()
    {
        // 禁用当前的Sprite
        gameObject.SetActive(false);

        // 启用另一个Sprite
        otherSprite.SetActive(true);
    }

    // 在发生碰撞时调用的方法
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 检查是否与标记为"YourTag"的其他游戏对象发生碰撞
        if (collision.gameObject.tag == "Player")
        {
            // 切换到另一个Sprite
            SwitchToOtherSprite();
            //Debug.Log("Drop");
        }
    }
}
