using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team32CameraFollow : MonoBehaviour
{
    public Transform player; 
    public float smoothSpeed = 0.125f; 
    public Vector3 offset; 

    public float leftLimit = -5.5f; 
    public float rightLimit = 5.5f; 

    void FixedUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        
        smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, leftLimit, rightLimit);

        transform.position = new Vector3(smoothedPosition.x, transform.position.y, transform.position.z);
    }
}