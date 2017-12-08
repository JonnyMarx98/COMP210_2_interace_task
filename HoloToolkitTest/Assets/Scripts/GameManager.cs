using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace HoloToolkit.Unity.InputModule
{
    public class GameManager : MonoBehaviour
    {
        AudioSource audioSource;
        public bool Playing;
        TapToPlace tapToPlace;
        public int score;
        public Text scoreText;

        // Use this for initialization
        void Start()
        {
            scoreText = GameObject.Find("Text").GetComponent<Text>();
            Playing = false;
            tapToPlace = gameObject.GetComponent<TapToPlace>();
            audioSource = gameObject.GetComponent<AudioSource>();
            score = 0;
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
            // Play sound/display text to let player know game is in place mode
        }

        public void OnReset()
        {
            SceneManager.LoadScene("FloorGame");
        }

        // Update is called once per frame
        void Update()
        {
            scoreText.text = ("Score: " + score.ToString());
        }
    }
}
