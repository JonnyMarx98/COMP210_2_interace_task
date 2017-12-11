using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunPickUp : MonoBehaviour {

    HoloToolkit.Unity.InputModule.Tests.Shoot shoot;
    HoloToolkit.Unity.InputModule.GameManager gameManager;
    public float timer = 10.0f;
    AudioSource audioSource;
    public AudioClip pickUpSound;
    GameObject player;   
    public GameObject UItext;

    // Use this for initialization
    void Start ()
    {
        player = GameObject.Find("Player");
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<HoloToolkit.Unity.InputModule.GameManager>();
        shoot = player.GetComponent<HoloToolkit.Unity.InputModule.Tests.Shoot>();
        audioSource = player.GetComponent<AudioSource>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            shoot.hasShotgun = true;
            shoot.shotgunTime = shoot.initialShotgunTime;
            audioSource.clip = pickUpSound;
            audioSource.Play();
            gameManager.DisplayText(UItext, "Shotgun!");
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update ()
    {
        timer -= Time.deltaTime;

        if (timer <= 0.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
