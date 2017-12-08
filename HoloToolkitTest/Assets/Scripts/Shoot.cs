using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

//public class Shoot : MonoBehaviour {

//	// Use this for initialization
//	void Start () {
		
//	}
	
//	// Update is called once per frame
//	void Update () {
		
//	}
//}

namespace HoloToolkit.Unity.InputModule.Tests
{
    // This class implements IInputClickHandler to handle the tap gesture.
    public class Shoot : MonoBehaviour, IInputHandler
    {
        public GameObject Bullet_Emitter;
        public GameObject Bullet;
        public float Bullet_Forward_Force;
        public AudioSource audioSource;
        public int Shots = 5;
        public float spreadFactor = 0.2f;
        public bool hasShotgun = false;
        public float shotgunTime = 5.0f;
        private float initialTime;
        public AudioClip handgunSound;
        public AudioClip shotgunSound;

        private void Awake()
        {
            audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.clip = handgunSound;
            initialTime = shotgunTime;
        }


        public void OnInputDown(InputEventData eventData)
        {
            if (hasShotgun)
            {
                ShotGun();
                audioSource.clip = shotgunSound;
            }
            else
            {
                HandGun();
                audioSource.clip = handgunSound;
            }
            eventData.Use(); // Mark the event as used, so it doesn't fall through to other handlers.
            audioSource.Play(); // Play Shoot sound
            
        }

        public void OnInputUp(InputEventData eventData)
        {
            //throw new NotImplementedException();
        }

        void HandGun()
        {
            GameObject Temporary_Bullet_Handler;
            Rigidbody Temporary_RigidBody;
            Temporary_Bullet_Handler = Instantiate(Bullet, Bullet_Emitter.transform.position, Bullet_Emitter.transform.rotation) as GameObject;

            //Retrieve the Rigidbody component from the instantiated Bullet and control it.
            Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();

            //Add force to bullet
            Temporary_RigidBody.AddForce(transform.forward * Bullet_Forward_Force);

            //print("(" + transform.forward.x + ", " + transform.forward.y + ", " + transform.forward.z + ")");

            //Destroy bullets after 4 seconds
            Destroy(Temporary_Bullet_Handler, 4.0f);             
        }

        void ShotGun()
        {
            GameObject Temporary_Bullet_Handler;
            for (int i = 0; i < Shots; i++)
            {
                Vector3 bulletDirection = (Bullet_Emitter.transform.forward);
                bulletDirection.x += UnityEngine.Random.Range(-spreadFactor, spreadFactor);
                bulletDirection.z += UnityEngine.Random.Range(-spreadFactor, spreadFactor);

                Temporary_Bullet_Handler = Instantiate(Bullet, Bullet_Emitter.transform.position, Bullet_Emitter.transform.rotation) as GameObject;

                //Retrieve the Rigidbody component from the instantiated Bullet and control it.
                Rigidbody Temporary_RigidBody;
                Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();

                //Add force to bullet
                Temporary_RigidBody.AddForce(bulletDirection * Bullet_Forward_Force); //new Vector3((transform.forward.x + 0.1f), transform.forward.y, transform.forward.z - 0.1f)
                audioSource.Play();
                //Destroy bullets after 4 seconds
                Destroy(Temporary_Bullet_Handler, 4.0f);
            }
        }

        private void Update()
        {
            if (hasShotgun)
            {
                shotgunTime -= Time.deltaTime;
                print(shotgunTime);
            }
            if (shotgunTime <= 0.0f)
            {
                hasShotgun = false;
                print("times up no shotty now");
                shotgunTime = initialTime;
            }
        }
    }

// Add full auto gun here 

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


