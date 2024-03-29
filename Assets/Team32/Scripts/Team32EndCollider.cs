using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace team32
{
    public class Team32EndCollider : MicrogameEvents
    {

        public bool hasWon;
        public bool isGameOver;

        protected override void OnGameStart()
        {
            hasWon = false;
            //Debug.Log("game starts");
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                //Debug.Log("you win!");
                isGameOver = true;
                ReportGameCompletedEarly();
                hasWon = true;
            }
        }

        protected override void OnTimesUp()
        {
            if (!hasWon)
            {
                //Debug.Log("you lose!");
                isGameOver = true;
            }
        }


    }
}
