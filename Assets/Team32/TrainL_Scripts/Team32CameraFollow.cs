using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team32CameraFollow : MonoBehaviour
{
    public Transform player; // ��ҵ�Transform
    public float smoothSpeed = 0.125f; // ����������ƽ���ٶ�
    public Vector3 offset; // ���������ҵ�ƫ����

    public float leftLimit = -5.5f; // �������߽�
    public float rightLimit = 5.5f; // ������ұ߽�

    void FixedUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // ������������ƶ���Χ
        smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, leftLimit, rightLimit);

        transform.position = new Vector3(smoothedPosition.x, transform.position.y, transform.position.z);
    }
}