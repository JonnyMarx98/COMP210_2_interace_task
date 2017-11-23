using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR.WSA.Input;

public class PlayerMovement : MonoBehaviour {

    
    Animator anim;

    public float speed = 5;
    private Vector3 targetPosition;
    private bool isMoving;
    bool isWalking;
    private Vector3 cursorPosition;
    GestureRecognizer recognizer;

    // Use this for initialization
    void Start()
    {
        // Set up a GestureRecognizer to detect tap gestures.
        recognizer = new GestureRecognizer();
        recognizer.TappedEvent += MovePlayer;
        recognizer.StartCapturingGestures();

        anim = GameObject.Find("Player_Idle").GetComponent<Animator>();
        anim.speed = speed / 3;

        targetPosition = transform.position; //+ new Vector3(3.0f, 0.0f, 3.0f);
        isMoving = false;
        isWalking = true;
    }

    private void MovePlayer(InteractionSourceKind source, int tapCount, Ray headRay)
    {
        cursorPosition = GameObject.Find("Cursor").transform.position;
        targetPosition = cursorPosition;
        targetPosition.y = transform.position.y; // -3.0f;
        isMoving = true;
        
        transform.LookAt(targetPosition);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isWalking)
            {
                isWalking = false;
            }
            else
            {
                isWalking = true;
            }
            
        }

        if (isWalking)
        {
            MovePlayer(InteractionSourceKind.Other, 1, new Ray());
            anim.SetBool("IsWalking", true);
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }

        // always look at cursor 
        cursorPosition = GameObject.Find("Cursor").transform.position;
        targetPosition = cursorPosition;
        transform.LookAt(targetPosition);

        // lock z and x rotation
        Quaternion lockRotation = new Quaternion(0.0f, transform.rotation.y,0.0f, transform.rotation.w);
        transform.rotation = lockRotation;

        //if (isMoving)
        //{
        //    MovePlayer(InteractionSourceKind.Other, 1, new Ray());
        //}



    }
}
