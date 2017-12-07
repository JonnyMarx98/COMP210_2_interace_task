using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Shoot : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

namespace HoloToolkit.Unity.InputModule.Tests
{
    // This class implements IInputClickHandler to handle the tap gesture.
    public class Shoot : MonoBehaviour, IInputHandler
    {
        public GameObject Bullet_Emitter;
        public GameObject Bullet;
        public float Bullet_Forward_Force;
        public AudioSource audioSource;

        private void Awake()
        {
            audioSource = gameObject.GetComponent<AudioSource>();
        }


        public void OnInputDown(InputEventData eventData)
        {
            GameObject Temporary_Bullet_Handler;
            Temporary_Bullet_Handler = Instantiate(Bullet, Bullet_Emitter.transform.position, Bullet_Emitter.transform.rotation) as GameObject;

            //Sometimes bullets may appear rotated incorrectly due to the way its pivot was set from the original modeling package.
            //This is EASILY corrected here, you might have to rotate it from a different axis and or angle based on your particular mesh.
            //Temporary_Bullet_Handler.transform.Rotate(Vector3.left * 90);

            //Retrieve the Rigidbody component from the instantiated Bullet and control it.
            Rigidbody Temporary_RigidBody;
            Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();

            //Tell the bullet to be "pushed" forward by an amount set by Bullet_Forward_Force.
            Temporary_RigidBody.AddForce(transform.forward * Bullet_Forward_Force);

            //Basic Clean Up, set the Bullets to self destruct after 10 Seconds, I am being VERY generous here, normally 3 seconds is plenty.
            Destroy(Temporary_Bullet_Handler, 4.0f);

            audioSource.Play();

            eventData.Use(); // Mark the event as used, so it doesn't fall through to other handlers.
        }

        public void OnInputUp(InputEventData eventData)
        {
            //throw new NotImplementedException();
        }
    }

//    public class Shoot : MonoBehaviour, IHoldHandler
//    {
//        private bool holding = false;
//        public void OnHoldCanceled(HoldEventData eventData)
//        {
//            holding = false;
//        }

//        public void OnHoldCompleted(HoldEventData eventData)
//        {
//            holding = false;
//        }

//        public void OnHoldStarted(HoldEventData eventData)
//        {
//            holding = true;
            
//            eventData.Use(); // Mark the event as used, so it doesn't fall through to other handlers.
//        }

//        private void Update()
//        {
//            if (holding)
//            {
//                gameObject.transform.localScale += 0.005f * gameObject.transform.localScale;
//            }
//        }
//}
}


