using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
     private Rigidbody rb;
     public bool isHit = false;
     public StickController stickController;


     // Start is called before the first frame update
     void Start()
     {
          rb = this.GetComponent<Rigidbody>();
     }

     // Update is called once per frame
     void Update()
     {

     }
    
     private void OnCollisionEnter(Collision collision)
     {
          if (collision.gameObject.CompareTag("stick") && !isHit)
          {
               isHit = true;
               Debug.Log("hit");
               LaunchBall();
          }

          
     }

     

     void LaunchBall()
     {
          float force = stickController.SwipeForce;
          Debug.Log(force);
          rb.AddForce(new Vector3(0, force, force), ForceMode.Impulse);
     }
}
