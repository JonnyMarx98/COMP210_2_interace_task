using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPickUp : MonoBehaviour {

    HoloToolkit.Unity.InputModule.Tests.Shoot shoot;
    HoloToolkit.Unity.InputModule.Tests.ShootAuto shootAuto;
    public float timer = 10.0f;
    AudioSource audioSource;
    public AudioClip pickUpSound;
    GameObject player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        shootAuto = player.GetComponent<HoloToolkit.Unity.InputModule.Tests.ShootAuto>();
        audioSource = player.GetComponent<AudioSource>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // display gun name
            shootAuto.hasAutoGun = true;
            audioSource.clip = pickUpSound;
            audioSource.Play();
            Destroy(this.gameObject);
        }
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
