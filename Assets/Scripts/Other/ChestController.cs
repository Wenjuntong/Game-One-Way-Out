using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    public GameObject[] dropItems; // 要掉落的物品Prefab数组

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 随机选择一个物品Prefab
            GameObject dropItem = dropItems[Random.Range(0, dropItems.Length)];

            // 在宝箱位置生成掉落物品
            Vector3 dropPosition = transform.position + Vector3.down * 0.5f;
            Instantiate(dropItem, transform.position, Quaternion.identity);

        }
    }
}