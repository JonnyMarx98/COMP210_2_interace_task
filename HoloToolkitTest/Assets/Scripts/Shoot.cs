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
        public bool hasAutoGun = false;
        public float shotgunTime = 5.0f;
        public float autoTime = 5.0f;
        public float initialShotgunTime;       
        public float initialAutogunTime;
        [SerializeField]
        public AudioClip handgunSound;
        public AudioClip shotgunSound;
        //ShootAuto shootAuto;

        private float lastAutoShot;
        private float autoDelay = 0.1f;

        private void Awake()
        {
            audioSource = gameObject.GetComponent<AudioSource>();
           // shootAuto = gameObject.GetComponent<ShootAuto>();
            audioSource.clip = handgunSound;
            lastAutoShot = 0.0f;
            initialShotgunTime = shotgunTime;
            initialAutogunTime = autoTime;
        }


        public void OnInputDown(InputEventData eventData)
        {
            if (!hasAutoGun)
            {
                if (hasShotgun)
                {
                    ShotGun();
                }
                else
                {
                    HandGun();
                }
                audioSource.Play();
            }            
            eventData.Use(); // Mark the event as used, so it doesn't fall through to other handlers.
            //audioSource.Play(); // Play Shoot sound
            
        }

        public void OnInputUp(InputEventData eventData)
        {
            //throw new NotImplementedException();
        }

        public void HandGun()
        {
            GameObject Temporary_Bullet_Handler;
            Rigidbody Temporary_RigidBody;
            Temporary_Bullet_Handler = Instantiate(Bullet, Bullet_Emitter.transform.position, Bullet_Emitter.transform.rotation) as GameObject;

            //Retrieve the Rigidbody component from the instantiated Bullet and control it.
            Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();
            //Add force to bullet
            Temporary_RigidBody.AddForce(transform.forward * Bullet_Forward_Force);

            audioSource.clip = handgunSound;
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
            audioSource.clip = shotgunSound;
        }

        private void Update()
        {

            if (hasShotgun)
            {
                hasAutoGun = false;
                AudioSource audioSrc = GameObject.Find("MiniGunAudio").GetComponent<AudioSource>();
                audioSrc.Stop();
                shotgunTime -= Time.deltaTime;
                print(shotgunTime);
            }
            if (shotgunTime <= 0.0f)
            {
                hasShotgun = false;
                print("times up no shotty now");
                shotgunTime = initialShotgunTime;
            }


            if (hasAutoGun)
            {
                hasShotgun = false;
                if (lastAutoShot + autoDelay < Time.fixedTime)
                {
                    HandGun();
                    audioSource.Play();
                    lastAutoShot = Time.fixedTime;
                }
                autoTime -= Time.deltaTime;
                print("lastShot = " + lastAutoShot);
                print("delta = " + Time.fixedTime);
            }
            if (autoTime <= 0.0f)
            {
                hasAutoGun = false;
                autoTime = initialAutogunTime;
            }
        }
    }
}


