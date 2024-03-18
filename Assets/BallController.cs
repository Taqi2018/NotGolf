using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody rb;
    public int force;

    // Start is called before the first frame update
    void Start()
    {
       rb= this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*    private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.name == "stick")
            {
                Debug.Log("Hit");
                rb.AddForce(new Vector3(0, 1 * force, 1 * force), ForceMode.Impulse);
            }

        }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "stick")
        {
            Debug.Log("Hit");
            rb.isKinematic = false;
            rb.AddForce(new Vector3(0, 2* force, 1 * force), ForceMode.Impulse);
            this.GetComponent<SphereCollider>().isTrigger = false;
        }

    }
}
