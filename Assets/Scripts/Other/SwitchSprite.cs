using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSprite : MonoBehaviour
{
    // 要切换到的另一个Sprite
    public GameObject otherSprite;
    public GameObject[] dropItems; // 要掉落的物品Prefab数组

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
            // 随机选择一个物品Prefab
            GameObject dropItem = dropItems[Random.Range(0, dropItems.Length)];

            // 在宝箱下方生成掉落物品
            //Vector3 dropPosition = transform.position + Vector3.up * 0.75f;
            // 在玩家左侧生成掉落物品
            Vector3 playerPosition = collision.transform.position; 
            Vector3 dropPosition = playerPosition + Vector3.left * 0.5f; 
            //Instantiate(dropItem, dropPosition, Quaternion.identity);
            Instantiate(dropItem, dropPosition, Quaternion.identity);

            // 切换到另一个Sprite
            SwitchToOtherSprite();
            //Debug.Log("Drop");
        }
    }

}
