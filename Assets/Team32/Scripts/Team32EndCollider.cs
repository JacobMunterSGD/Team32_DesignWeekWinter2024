using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team32EndCollider : MicrogameEvents
{

    bool hasWon;

    protected override void OnGameStart()
    {
        hasWon = false;
        Debug.Log("game starts");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("you win!");
            hasWon = true;
        }
    }

    protected override void OnTimesUp()
    {
        if (!hasWon)
        {
            Debug.Log("you lose!");
        }
    }
}
