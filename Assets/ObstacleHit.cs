using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleHit : MonoBehaviour
{

     public int MoneyDeductionAmount = 50;


     private void OnCollisionEnter(Collision collision)
     {
          if (collision.gameObject.CompareTag("GolfBall"))
          {
               // Deduct money from the player
               int currentMoney = PlayerPrefs.GetInt("Money", 0);
               currentMoney -= MoneyDeductionAmount;
               if (currentMoney < 0)
               {
                    currentMoney = 0;
               }

               PlayerPrefs.SetInt("Money", currentMoney);

               Debug.Log("Money deducted. Current money: " + currentMoney);

             
          }
     }
}
