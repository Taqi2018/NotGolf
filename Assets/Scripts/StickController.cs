using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using UnityEngine.InputSystem.Controls;

public class StickController : MonoBehaviour
{
     InputHub swipeInput;
     public float startXAngle;

     Vector2 initialPos;
     private bool isTouch;
     public float swipePowerMultiplier = 2f;
     float currentRotation;
     private bool getTapPos;
     public float rotationSpeed;
     public float SwipeForce;
     public Touch Touch { get; private set; }
     public bool isHit = false;
     public float ballAngulardrag = 2f;

     // Start is called before the first frame update
     void Start()
     {
          swipeInput = new InputHub();
          swipeInput.Enable();
          swipeInput.BallSwipe.Tap.started += OnSwipeStart;
          swipeInput.BallSwipe.Tap.canceled += OnSwipeEnd;
          swipeInput.BallSwipe.TapPosition.started += OnTapPos;
          swipeInput.BallSwipe.TapPosition.performed -= OnTapPosEnd;
          getTapPos = false;

     }

     private void OnTapPos(InputAction.CallbackContext obj)
     {
          getTapPos = true;

          initialPos = swipeInput.BallSwipe.TapPosition.ReadValue<Vector2>();

          isTouch = true;
     }

     private void OnTapPosEnd(InputAction.CallbackContext obj)
     {
          isTouch = false;
     }

     private void OnSwipeEnd(InputAction.CallbackContext obj)
     {
          isTouch = false;
          swipeInput.Disable();
          swipeInput.Enable();
     }

     private void OnSwipeStart(InputAction.CallbackContext obj)
     {
          this.transform.rotation = Quaternion.Euler(startXAngle, 0, 0);
          if (getTapPos)
          {
               initialPos = swipeInput.BallSwipe.TapPosition.ReadValue<Vector2>();

               getTapPos = false;
          }

     }

     // Update is called once per frame
     void Update()
     {
          if (isTouch)
          {
               Vector2 currentPos = swipeInput.BallSwipe.TapPosition.ReadValue<Vector2>();

               Vector2 swipeDelta = currentPos - initialPos;

               if (swipeDelta.y != 0)
               {
                    currentRotation = transform.rotation.eulerAngles.x - swipeDelta.y * Time.deltaTime * rotationSpeed;

                    SwipeForce = swipeDelta.y * Time.deltaTime * rotationSpeed;

                    float clampedRotation;

                    if (currentRotation > 180f)
                         currentRotation -= 360f;
                    else if (currentRotation < -180f)
                         currentRotation += 360f;   //  -180  -60    90   180 

                    // Clamp rotation between -60 and 90 degrees
                    clampedRotation = Mathf.Clamp(currentRotation, -60f, 90f);

                    transform.rotation = Quaternion.Euler(clampedRotation, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
               }
               initialPos = currentPos;
          }
     }

     private void OnTriggerEnter(Collider other)
     {
          if (other.CompareTag("GolfBall") && !isHit)
          {
               isHit = true;
               Debug.Log("hit");
               LaunchBall(other.attachedRigidbody);
          }
     }

     void LaunchBall(Rigidbody rb)
     {
          float force = SwipeForce;
          Debug.Log(force);
          rb.AddForce(new Vector3(0, force/2 , force), ForceMode.Impulse);
          rb.angularDrag = ballAngulardrag;
     }

     public void OnDestroy()
     {
          swipeInput.Disable();
     }
}




