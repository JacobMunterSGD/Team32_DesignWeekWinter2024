using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team32PlayerDog : MicrogameInputEvents
{

    Rigidbody2D rb;
    float moveSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = 4;
        gameObject.GetComponent<Collider2D>().enabled = false;
        

    }

    private void FixedUpdate()
    {
        float moveBy = stick.x * Time.deltaTime * moveSpeed;
        if (Mathf.Abs(rb.position.x + moveBy) > 5.5) return;
        rb.MovePosition(rb.position + new Vector2(moveBy, 0));

    }

    void Update()
    {
        if (stick.y == 1)
        {
            gameObject.GetComponent<Collider2D>().enabled = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("hit");
    }

}