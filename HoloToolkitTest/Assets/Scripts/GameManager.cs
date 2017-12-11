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
        private GameObject textIns;
        private GameObject[] UIobjects;

        // Use this for initialization
        void Start()
        {
            scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
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
            SceneManager.UnloadSceneAsync("FloorGame");
            SceneManager.LoadScene("FloorGame");
        }

        public void DisplayText(GameObject prefab, string text)
        {
            textIns = Instantiate(prefab);
            textIns.GetComponent<Text>().text = text;
            textIns.transform.SetParent(GameObject.Find("Canvas").transform);
            textIns.transform.localPosition = new Vector3(0.0f, 2.0f, 0.0f);
        }

        // Update is called once per frame
        void Update()
        {
            UIobjects = GameObject.FindGameObjectsWithTag("UItext");
            scoreText.text = ("Score: " + score.ToString());
            textIns.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f) * Time.deltaTime;
            textIns.transform.localRotation = GameObject.Find("Canvas").transform.rotation;
            if (textIns.transform.localScale.x > 2.5f)
            {
                // destroys all UItext to prevent text that spawned at same time from staying on screen
                for (int i = 0; i < UIobjects.Length; i++)
                {
                    Destroy(UIobjects[i]);
                }
            }
        }
    }
}
