using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player" && other.gameObject.tag != "Bullet")
        {
            Destroy(this.gameObject);
        }

    }

    // Use this for initialization
    void Start ()
    {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
