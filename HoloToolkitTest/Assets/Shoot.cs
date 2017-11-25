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
    /// This class implements IInputClickHandler to handle the tap gesture.
    //public class Shoot : MonoBehaviour, IInputHandler
    //{
    //    public void OnInputDown(InputEventData eventData)
    //    {
    //        // Increase the scale of the object just as a response.
    //        gameObject.transform.localScale += 0.05f * gameObject.transform.localScale;

    //        eventData.Use(); // Mark the event as used, so it doesn't fall through to other handlers.
    //    }

    //    public void OnInputUp(InputEventData eventData)
    //    {
    //        //throw new NotImplementedException();
    //    }
    //}

    public class Shoot : MonoBehaviour, IHoldHandler
    {
        private bool holding = false;
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

        private void Update()
        {
            if (holding)
            {
                gameObject.transform.localScale += 0.005f * gameObject.transform.localScale;
            }
        }
}
}


