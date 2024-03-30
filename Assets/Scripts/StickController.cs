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
     public Vector3 SwipeForce;
     public Touch Touch { get; private set; }
     public bool isHit = false;
     public float ballAngulardrag = 2f;
    private Vector2 joyStickMovement;

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
          /*this.transform.localRotation= Quaternion.Euler(startXAngle, 0, 0);*/
          if (getTapPos)
          {
               initialPos = swipeInput.BallSwipe.TapPosition.ReadValue<Vector2>();

               getTapPos = false;
          }

     }

     // Update is called once per frame
     void Update()
     {
        joyStickMovement = swipeInput.FindAction("Joystick").ReadValue<Vector2>();

        if(joyStickMovement.x != 0)
        {
            currentRotation = transform.rotation.eulerAngles.y - joyStickMovement.x*2 * Time.deltaTime * rotationSpeed;
             transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, currentRotation, transform.rotation.eulerAngles.z);

            transform.parent.rotation= Quaternion.Euler(transform.parent.rotation.eulerAngles.x, currentRotation, transform.parent.rotation.eulerAngles.z);
        }


       Debug.Log( swipeInput.FindAction("Joystick").ReadValue<Vector2>());
          if (isTouch & joyStickMovement==Vector2.zero)
          {
               Vector2 currentPos = swipeInput.BallSwipe.TapPosition.ReadValue<Vector2>();

               Vector2 swipeDelta = currentPos - initialPos;

   

  /*          if (swipeDelta.x != 0)
            {
                Debug.Log(swipeDelta.x);
                currentRotation = transform.rotation.eulerAngles.y - swipeDelta.x * Time.deltaTime * rotationSpeed;

                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, currentRotation, transform.rotation.eulerAngles.z);
*/

                if (swipeDelta.y != 0)
                {
                    currentRotation = transform.rotation.eulerAngles.x - swipeDelta.y * Time.deltaTime * rotationSpeed;

                SwipeForce = new Vector3(swipeDelta.x * Time.deltaTime * rotationSpeed, swipeDelta.y * Time.deltaTime * rotationSpeed, swipeDelta.y * Time.deltaTime * rotationSpeed);

                    float clampedRotation;

                    if (currentRotation > 180f)
                        currentRotation -= 360f;
                    else if (currentRotation < -180f)
                        currentRotation += 360f;   //  -180  -60    90   180 

                    // Clamp rotation between -60 and 90 degrees
                    clampedRotation = Mathf.Clamp(currentRotation, -60f, 90f);

                    transform.rotation = Quaternion.Euler(clampedRotation, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
                }


       /*     }
*/
      


            initialPos = currentPos;
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
         /* float force = SwipeForce;*/
    /*      Debug.Log(force);*/
          rb.AddForce(new Vector3(SwipeForce.x, SwipeForce.y/2 , SwipeForce.z), ForceMode.Impulse);
          rb.angularDrag = ballAngulardrag;
          rb.angularVelocity = new Vector3(0f, 2f, 0f);
     }

     public void OnDestroy()
     {
          swipeInput.Disable();
     }
}




