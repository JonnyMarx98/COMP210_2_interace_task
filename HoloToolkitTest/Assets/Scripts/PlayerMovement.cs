using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 2f;
    private float maxSpeed = 5f;
    private float minSpeed = 1f;
    private Vector3 targetPosition;
    //private bool isMoving;
    public bool isWalking;
    private Vector3 cursorPosition;
    Animator anim;

    // Use this for initialization
    void Start ()
    {
        isWalking = true;

        anim = GameObject.Find("Player_Anim").GetComponent<Animator>();
        anim.speed = speed;

	}

    private void MovePlayer()
    {
        cursorPosition = GameObject.Find("DefaultCursor").transform.position;
        targetPosition = cursorPosition;
        targetPosition.y = transform.position.y;

        transform.LookAt(targetPosition);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, (speed/7f) * Time.deltaTime);
    }

    public void OnMove()
    {
        isWalking = true;
    }
    public void OnStop()
    {
        isWalking = false;
    }
    public void OnFaster()
    {
        if (speed < maxSpeed)
        {
            speed += 1;
            if (speed > maxSpeed) { speed = maxSpeed; }
            anim.speed = speed;
        }

    }
    public void OnSlower()
    {
        if (speed > minSpeed)
        {
            speed -= 1;
            if (speed < minSpeed) { speed = minSpeed; }
            anim.speed = speed;
        }      
        
    }


    // Update is called once per frame
    void Update ()
    {
        
        if (isWalking)
        {
            MovePlayer();
            anim.SetBool("IsWalking", true);
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }

        if(transform.position.y < -100)
        {
            gameObject.GetComponent<PlayerHealth>().playerHealth = 0.0f;
        }

        // always look at cursor 
        cursorPosition = GameObject.Find("DefaultCursor").transform.position;
        targetPosition = cursorPosition;
        transform.LookAt(targetPosition);

        // lock z and x rotation
        Quaternion lockRotation = new Quaternion(0.0f, transform.rotation.y, 0.0f, transform.rotation.w);
        transform.rotation = lockRotation;
    }
}


