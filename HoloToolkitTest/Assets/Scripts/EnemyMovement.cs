using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    public float speed = 2;
    private Vector3 targetPosition;
    //private bool isMoving;
    public bool isWalking;
    private Vector3 playerPosition;
    GameObject player;
    Transform playerTransform;       // reference to player position

    // Use this for initialization
    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        float playerSpeed = player.GetComponent<PlayerMovement>().speed;
        playerTransform = player.transform;
        isWalking = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Destroy(this.gameObject);
        }
    }

    private void MovePlayer()
    {
        playerPosition = GameObject.Find("Player").transform.position;
        targetPosition = playerPosition;
        targetPosition.y = transform.position.y;

        transform.LookAt(targetPosition);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, (speed / 20f) * Time.deltaTime);
    }

    public void OnMove()
    {
        isWalking = true;
    }

    public void OnStop()
    {
        isWalking = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (isWalking)
        {
            MovePlayer();
        }

        // always look at player 
        playerPosition = GameObject.Find("Player").transform.position;
        targetPosition = playerPosition;
        transform.LookAt(targetPosition);

        // lock z and x rotation
        Quaternion lockRotation = new Quaternion(0.0f, transform.rotation.y, 0.0f, transform.rotation.w);
        transform.rotation = lockRotation;
    }
}
