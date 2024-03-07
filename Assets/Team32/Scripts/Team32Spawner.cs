using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace team32
{
    public class Team32Spawner : MicrogameInputEvents
    {

        public GameObject dog;
        public GameObject player;

        float timer;

        bool gameStarted;

        float spawnRangeX = 13f;
        float spawnOffsetY = 6f;
        //float spawnOffsetX = -10f; 

        float spawnTimerReset;

        protected override void OnGameStart()
        {
            timer = 1f;
            gameStarted = true;

            spawnTimerReset = .1f;

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
                float spawnX = Random.Range(playerX - spawnRangeX /*+ spawnOffsetX*/, playerX + spawnRangeX);
                Vector3 spawnPosition = new Vector3(spawnX, transform.position.y + spawnOffsetY, transform.position.z);

                // Instantiate the dog object and set the location
                float tempRandom = Random.Range(1, 3);
                if (tempRandom == 1)
                {
                    Instantiate(dog, spawnPosition, Quaternion.identity);
                }
                else if (tempRandom == 2)
                {
                    //Debug.Log("cat will go here later");
                    Instantiate(dog, spawnPosition, Quaternion.identity);
                }

                timer = spawnTimerReset; // Reset the generation interval
            }
        }

        protected override void OnFifteenSecondsLeft()
        {
            spawnTimerReset = .07f;
        }

        protected override void OnTenSecondsLeft()
        {
            spawnTimerReset = .04f;
        }
    }
}
