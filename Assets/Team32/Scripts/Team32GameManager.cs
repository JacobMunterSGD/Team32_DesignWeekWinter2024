using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace team32
{
    public class Team32GameManager : MicrogameEvents
    {

        public Team32EndCollider EndCollider;
        SpriteRenderer sr;

        public Sprite winScreen;
        public Sprite loseScreen;

        private void Start()
        {
            sr = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (EndCollider.hasWon)
            {
                // display win image
                sr.sprite = winScreen;

            }
        }

        protected override void OnTimesUp()
        {
            Debug.Log("times up");

            if (!EndCollider.hasWon)
            {
                //display lose image
                sr.sprite = loseScreen;

            }
        }
    }
}
