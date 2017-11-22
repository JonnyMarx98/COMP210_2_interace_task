using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR.WSA.Input;

public class PlayerMovement : MonoBehaviour {

    
    Animator anim;

    public float speed = 5;
    private Vector3 targetPosition;
    private bool isMoving;
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
        if (Input.GetKey(KeyCode.Space))
        {
            MovePlayer(InteractionSourceKind.Other, 1, new Ray());
            anim.SetBool("IsWalking", true);
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }
        

        //if (isMoving)
        //{
        //    MovePlayer(InteractionSourceKind.Other, 1, new Ray());
        //}



    }
}
