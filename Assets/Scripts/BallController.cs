using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
     private Rigidbody rb;
     // Start is called before the first frame update
     void Start()
     {
          rb = this.GetComponent<Rigidbody>();
     }


}
