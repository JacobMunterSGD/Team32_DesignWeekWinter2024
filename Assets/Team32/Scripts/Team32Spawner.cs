using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team32Spawner : MicrogameInputEvents
{

    public GameObject dog;

    float timer;

    bool gameStarted;

    protected override void OnGameStart()
    {
        timer = 2;
        gameStarted = true;
    }

    void Update()
    {
        if (!gameStarted) return;
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            Instantiate(dog);
            timer = .2f;
        }

    }
}
