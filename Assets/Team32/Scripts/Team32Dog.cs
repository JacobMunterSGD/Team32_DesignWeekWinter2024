using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team32Dog : MicrogameEvents
{
    Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //transform.position = new Vector3(Random.Range(-5.5f, 5.5f), 6, 0);
        rb.velocity = new Vector3(Random.Range(-1.5f, 1.5f), 0, 0);

    }

    private void FixedUpdate()
    {
        
    }
    void Update()
    {
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
