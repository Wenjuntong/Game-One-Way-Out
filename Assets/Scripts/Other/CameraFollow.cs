using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smoothSpeed = 0.125f; // 相机跟随的平滑速度
    public Vector3 offset; // 相机与目标之间的偏移量

    private Transform target; // 要跟随的目标的Transform组件
    private string targetTag = "Player"; // 要跟随的目标的tag

    private void Start()
    {
        // 查找所有带有targetTag标签的游戏对象，并选取第一个作为跟随目标
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);
        if (targets.Length > 0)
        {
            target = targets[0].transform;
        }
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            return; // 如果没有找到目标，不执行后面的代码
        }

        // 计算相机应该移动到的位置
        Vector3 desiredPosition = target.position + offset;

        // 使用平滑插值使相机移动更平滑
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // 设置相机的位置
        transform.position = smoothedPosition;
    }
}