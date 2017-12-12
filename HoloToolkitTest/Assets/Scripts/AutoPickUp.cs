using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPickUp : MonoBehaviour {

    HoloToolkit.Unity.InputModule.Tests.Shoot shoot;
    HoloToolkit.Unity.InputModule.GameManager gameManager;
    public float timer = 10.0f;
    AudioSource audioSource;
    public AudioClip pickUpSound;
    GameObject player;
    public GameObject UItext;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        shoot = player.GetComponent<HoloToolkit.Unity.InputModule.Tests.Shoot>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<HoloToolkit.Unity.InputModule.GameManager>();
        audioSource = player.GetComponent<AudioSource>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // display gun name
            shoot.hasShotgun = false;
            shoot.hasAutoGun = true;
            shoot.autoTime = shoot.initialAutogunTime;
            audioSource.clip = pickUpSound;
            audioSource.Play();
            PlayerDamageAudio();
            gameManager.DisplayText(UItext, "Minigun!");
            Destroy(this.gameObject);
        }
    }

    private void PlayerDamageAudio()
    {
        AudioSource audioSrc = GameObject.Find("MiniGunAudio").GetComponent<AudioSource>();
        audioSrc.Play();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
