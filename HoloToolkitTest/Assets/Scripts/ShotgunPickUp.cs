using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunPickUp : MonoBehaviour {

    HoloToolkit.Unity.InputModule.Tests.Shoot shoot;
    public float weaponTime = 5.0f;
    private float InitialTime;
    private bool startTimer = false;

	// Use this for initialization
	void Start ()
    {
        shoot = GameObject.Find("Player").GetComponent<HoloToolkit.Unity.InputModule.Tests.Shoot>();
        InitialTime = weaponTime;
	}

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            shoot.hasShotgun = true;
            startTimer = true;
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update ()
    {
		if (startTimer)
        {
            weaponTime -= Time.deltaTime;
            print(weaponTime);
        }
        if (weaponTime <= 0.0f)
        {
            shoot.hasShotgun = false;
            print("times up no shotty now");
            weaponTime = InitialTime;
        }
	}
}
