using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    public Weapon weapon;

    AudioSource source;

    void Start()
    {
        source = GameObject.FindGameObjectWithTag("UIManager").GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            
        }
    }
}
