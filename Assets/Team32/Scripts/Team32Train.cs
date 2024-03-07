using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace team32
{
    public class Team32Train : MicrogameEvents
    {

        float speed = 9;
        float acceleration = -.5f;

        void Start()
        {
            transform.position = new Vector2(20, 2.5f);
        }

        void FixedUpdate()
        {

            

            if (speed > 0)
            {
                transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
                speed += acceleration * Time.deltaTime;
            }

        }
    }
}