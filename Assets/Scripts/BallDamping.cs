using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDamping : MonoBehaviour
{
     public Rigidbody ballRigidbody;
     public float dampingFactor = 0.95f;
     public float stopThreshold = 0.1f;

     void Update()
     {
          if (ballRigidbody.angularDrag > stopThreshold)
          {
               // Reduce angular velocity over time
               ballRigidbody.angularVelocity *= dampingFactor;
               
          }
          else
          {
               // If angular velocity is below threshold, stop the rotation
               ballRigidbody.angularVelocity = Vector3.zero;
              
          }
     }
}
