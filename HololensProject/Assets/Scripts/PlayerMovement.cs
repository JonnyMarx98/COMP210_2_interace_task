using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR.WSA.Input;

public class PlayerMovement : MonoBehaviour {

    private float speed = 3;
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

        //cursorPosition = GameObject.Find("Cursor").transform.position;

        targetPosition = transform.position; //+ new Vector3(3.0f, 0.0f, 3.0f);
        isMoving = false;
    }

    private void MovePlayer(InteractionSourceKind source, int tapCount, Ray headRay)
    {
        cursorPosition = GameObject.Find("Cursor").transform.position;
        targetPosition = cursorPosition;
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
        }

        //if (isMoving)
        //{
        //    MovePlayer(InteractionSourceKind.Other, 1, new Ray());
        //}


        
    }
}
