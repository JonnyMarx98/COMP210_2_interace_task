﻿using UnityEngine;
using UnityEngine.VR.WSA.Input;

public class Shoot : MonoBehaviour
{

    GestureRecognizer recognizer;
    public float ForceMagnitude = 300f;


    // Use this for initialization
    void Start()
    {
        // Set up a GestureRecognizer to detect tap gestures.
        recognizer = new GestureRecognizer();
        recognizer.TappedEvent += ShootBall;
        recognizer.SetRecognizableGestures(GestureSettings.Tap | GestureSettings.Hold);
        recognizer.StartCapturingGestures();
    }

    private void ShootBall(InteractionSourceKind source, int tapCount, Ray headRay)
    {
        // Create a ball/sphere to shoot
        var ball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        // Resize the ball
        ball.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        // Make the ball adhere to gravity
        var rigidBody = ball.AddComponent<Rigidbody>();
        // Make the ball a little "floaty", like a ping pong ball
        rigidBody.mass = 0.5f;
        // Set the ball at the current position
        rigidBody.position = transform.position;
        // Then point the ball to go forward
        var transformForward = transform.forward;
        // Set the "shoot" angle
        transformForward = Quaternion.AngleAxis(-10, transform.right) * transformForward;
        // Shoot!!
        rigidBody.AddForce(transformForward * ForceMagnitude);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            ShootBall(InteractionSourceKind.Other, 1, new Ray());
        }
    }
    
    public void OnShoot()
    {
        ShootBall(InteractionSourceKind.Voice, 1, new Ray());
    }
}