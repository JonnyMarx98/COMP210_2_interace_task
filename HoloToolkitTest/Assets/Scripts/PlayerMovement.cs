using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 2;
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

        // always look at cursor 
        cursorPosition = GameObject.Find("DefaultCursor").transform.position;
        targetPosition = cursorPosition;
        transform.LookAt(targetPosition);

        // lock z and x rotation
        Quaternion lockRotation = new Quaternion(0.0f, transform.rotation.y, 0.0f, transform.rotation.w);
        transform.rotation = lockRotation;
    }
}


