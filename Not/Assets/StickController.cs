using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StickController : MonoBehaviour
{
    InputHub swipeInput;
    Vector2 initalPos;
    private bool isTouch;
    private Quaternion rotationX;

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

        initalPos = swipeInput.BallSwipe.TapPosition.ReadValue<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {


        if (isTouch)
        {
            Vector2 currentPos = swipeInput.BallSwipe.TapPosition.ReadValue<Vector2>();
            Vector2 swipeDelta = currentPos - initalPos;

          
            float rotationAmount = swipeDelta.y * Time.deltaTime * 10f; 

            transform.Rotate(Vector3.right, rotationAmount);

         
            initalPos= currentPos;
        }


        /*        Debug.Log("Move");
                rotationX = Quaternion.Euler(Touch.deltaPosition.y * 10, 0f, 0f);
                Debug.Log(rotationX);
                transform.rotation = rotationX * transform.rotation;*/

    }
}
