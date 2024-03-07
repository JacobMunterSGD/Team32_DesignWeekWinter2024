using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace team32
{
    public class Team32AudioManager : MicrogameEvents
    {

        public AudioSource umbrellaOpen;
        public AudioSource umbrellaClose;
        public AudioSource rain;
        public AudioSource dogBark;
        public AudioSource music;


        private void Start()
        {
            rain.Play();
        }
        protected override void OnGameStart()
        {
            //rain.Play();
            music.Play();

        }

        public void umbrellaOpenFunction()
        {
            umbrellaOpen.Play();
        }

        public void umbrellaCloseFunction()
        {
            umbrellaClose.Play();
        }

        public void rainFunction()
        {
            rain.Play();
        }

        public void dogBarkFunction()
        {
            dogBark.Play();
        }

        public void dogBarkStop()
        {
            dogBark.Stop();
        }
    }
}
