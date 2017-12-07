using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace HoloToolkit.Unity.InputModule
{
    public class GameManager : MonoBehaviour
    {
        AudioSource audioSource;
        public bool Playing;
        TapToPlace tapToPlace;

        // Use this for initialization
        void Start()
        {
            Playing = false;
            tapToPlace = gameObject.GetComponent<TapToPlace>();
            audioSource = gameObject.GetComponent<AudioSource>();
        }

        public void OnStart()
        {
            Playing = true;
            tapToPlace.enabled = false;
            // Play sound/display text to let player know game has started
        }

        public void OnPlace()
        {
            Playing = false;
            tapToPlace.Playing = false;
            // tapToPlace.enabled = true;
            // Play sound/display text to let player know game is in place mode
        }

        public void OnReset()
        {
            SceneManager.LoadScene("FloorGame");
        }

        // Update is called once per frame
        void Update()
        {
            if (!Playing)
            {

            }

        }
    }
}
