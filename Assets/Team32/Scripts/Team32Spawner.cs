using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team32Spawner : MicrogameInputEvents
{

    public GameObject dog;
    public GameObject player;

    float timer;

    bool gameStarted;

    float spawnRangeX = 5.5f; 
    float spawnOffsetY = 6f; 
    float spawnOffsetX = -10f; 

    protected override void OnGameStart()
    {
        timer = 2f;
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
            // Calculate the generation position so that it follows the player
            float playerX = player.transform.position.x; // Get the player's current position
            float spawnX = Random.Range(playerX - spawnRangeX + spawnOffsetX, playerX + spawnRangeX); 
            Vector3 spawnPosition = new Vector3(spawnX, transform.position.y + spawnOffsetY, transform.position.z); 

            // Instantiate the dog object and set the location
            Instantiate(dog, spawnPosition, Quaternion.identity);

            timer = .15f; // Reset the generation interval
        }
    }
}
