using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HoloToolkit.Unity.InputModule.Tests
{
    public class ShootAuto : MonoBehaviour, IHoldHandler
    {
        private float lastShotTime;
        private float shotDelay = 0.1f;
        public bool hasAutoGun = false;
        private bool holding = false;
        public float autoTime;
        private float initialAutogunTime;
        Shoot shoot;

        private void Awake()
        {
            shoot = gameObject.GetComponent<Shoot>();
            initialAutogunTime = autoTime;
        }

        public void OnHoldCanceled(HoldEventData eventData)
        {
            holding = false;
        }

        public void OnHoldCompleted(HoldEventData eventData)
        {
            holding = false;
        }

        public void OnHoldStarted(HoldEventData eventData)
        {
            holding = true;
            eventData.Use(); // Mark the event as used, so it doesn't fall through to other handlers.
        }

        void Update()
        {
            if (hasAutoGun)
            {
                if (holding && (lastShotTime + shotDelay) < Time.fixedTime)
                {
                    lastShotTime = Time.fixedTime;
                    shoot.HandGun();
                    shoot.audioSource.Play();
                }
                autoTime -= Time.deltaTime;
            }
            if (autoTime <= 0.0f)
            {
                hasAutoGun = false;
                //print("times up no shotty now");
                autoTime = initialAutogunTime;
            }
        }
    }
}
// gameObject.transform.localScale += 0.005f * gameObject.transform.localScale;