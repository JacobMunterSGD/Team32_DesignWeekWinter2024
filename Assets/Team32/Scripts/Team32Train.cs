using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace team32
{
    public class Team32Train : MonoBehaviour
    {

        float speed = 9;
        float acceleration = -.5f;

        void Start()
        {

        }

        void FixedUpdate()
        {

            

            if (speed > 0)
            {
                transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, 0);
                speed += acceleration * Time.deltaTime;
            }

        }
    }
}