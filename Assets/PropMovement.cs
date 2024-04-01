using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropMovement : MonoBehaviour
{
     public Transform[] waypoints;
     public float moveSpeed = 3f;

     private void Start()
     {
          StartCoroutine(MoveTowardsRandomWaypoint());
     }

     private IEnumerator MoveTowardsRandomWaypoint()
     {
          while (true)
          {
               // Pick a random waypoint
               int randomIndex = Random.Range(0, waypoints.Length);
               Transform targetWaypoint = waypoints[randomIndex];

               // Move towards the waypoint
               while (Vector3.Distance(transform.position, targetWaypoint.position) > 0.1f)
               {

                    transform.LookAt(targetWaypoint.position);
                    transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, moveSpeed * Time.deltaTime);
                    
                    yield return null;
               }

               // Wait for a short duration before selecting the next waypoint
               yield return new WaitForSeconds(0f);
          }
     }
}
