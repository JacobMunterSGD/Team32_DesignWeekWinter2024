using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team32CameraFollow : MonoBehaviour
{
    public Transform player; // 玩家的Transform
    public float smoothSpeed = 0.125f; // 摄像机跟随的平滑速度
    public Vector3 offset; // 摄像机与玩家的偏移量

    public float leftLimit = -5.5f; // 摄像机左边界
    public float rightLimit = 5.5f; // 摄像机右边界

    void FixedUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // 限制摄像机的移动范围
        smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, leftLimit, rightLimit);

        transform.position = new Vector3(smoothedPosition.x, transform.position.y, transform.position.z);
    }
}