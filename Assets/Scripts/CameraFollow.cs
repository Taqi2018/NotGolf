using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
     public Transform target; // Reference to the ball's transform
     public Vector3 offset;   // Offset between the camera and the ball

     void LateUpdate()
     {
          if (target != null)
          {
               // Update the camera's position to follow the ball with the specified offset
               transform.position = target.position + offset;
          }
     }
}
