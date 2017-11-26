using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    GameObject player;
    Transform playerTransform;       // reference to player position
    NavMeshAgent nav;

    // Use this for initialization
    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        float playerSpeed = player.GetComponent<PlayerMovement>().speed;
        playerTransform = player.transform;
        nav = GetComponent<NavMeshAgent>();
        nav.speed = playerSpeed / 10;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update ()
    {
        nav.SetDestination(playerTransform.position);
	}
}
