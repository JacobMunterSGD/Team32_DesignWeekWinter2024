using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace team32
{
    public class Team32Dog : MicrogameEvents
    {
        Rigidbody2D rb;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();

            //transform.position = new Vector3(Random.Range(-5.5f, 5.5f), 6, 0);
            rb.velocity = new Vector3(Random.Range(-2f, 2f), 0, 0);

        }

        private void FixedUpdate()
        {

        }
        void Update()
        {
            if (transform.position.y < -7)
            {
                Destroy(gameObject);
            }
        }
    }
}
