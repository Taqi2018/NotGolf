using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StickController : MonoBehaviour
{
     InputHub swipeInput;
     Vector2 initialPos;
     private bool isTouch;
     public float swipePowerMultiplier = 10f;

     public float SwipeForce { get; private set; }

     public Touch Touch { get; private set; }

     // Start is called before the first frame update
     void Start()
     {
          swipeInput = new InputHub();
          swipeInput.Enable();
          swipeInput.BallSwipe.Tap.started += OnSwipeStart;
          swipeInput.BallSwipe.Tap.canceled += OnSwipeEnd;

     }

     private void OnSwipeEnd(InputAction.CallbackContext obj)
     {
          Debug.Log("SwipeEnd");

          isTouch = false;

         
     }

     private void OnSwipeStart(InputAction.CallbackContext obj)
     {
          Debug.Log("SwipeStart");
          isTouch = true;
          initialPos = swipeInput.BallSwipe.TapPosition.ReadValue<Vector2>();
     }


     // Update is called once per frame
     void Update()
     {


          if (isTouch)
          {
               Vector2 currentPos = swipeInput.BallSwipe.TapPosition.ReadValue<Vector2>();
               Vector2 swipeDelta = currentPos - initialPos;
               SwipeForce = swipeDelta.magnitude * swipePowerMultiplier; // Calculate swipe force

               float rotationAmount = -1 * swipeDelta.y * Time.deltaTime * 20f;

               float currentRotation = transform.rotation.eulerAngles.x + rotationAmount;

               if (currentRotation > 180f)
                    currentRotation -= 360f;
               else if (currentRotation < -180f)
                    currentRotation += 360f;


               // Clamp rotation between -60 and 90 degrees
               float clampedRotation = Mathf.Clamp(currentRotation, -60f, 90f);

               transform.rotation = Quaternion.Euler(clampedRotation, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

               initialPos = currentPos;
          }

     }

     

}
