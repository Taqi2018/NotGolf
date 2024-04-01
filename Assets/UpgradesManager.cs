using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesManager : MonoBehaviour
{



     public GameObject ball;
     public int ballBounceMoneyRate;
     public GameObject stickObject;
     private bool isSwingEnable;
     private bool isBallSwipe;
     public Text ClubPowerLevel;
     public Text RocketBoosterLevel;
     public Text BallBounceLevel;


     // Start is called before the first frame update
     void Start()
     {
          PlayerPrefs.SetInt("BallBounceMoney", 100);

          ball.GetComponent<SphereCollider>().material.bounciness = PlayerPrefs.GetFloat("BallBounce", 0);

          Debug.Log(PlayerPrefs.GetInt("Money", 0));
          /*    PlayerPrefs.SetInt("RocketBoosterEffect", 20);
              PlayerPrefs.SetFloat("BallBounce",0.7f);
              PlayerPrefs.SetFloat("Power", 0.3f);
      */
          StickController.OnBallLaunched += SetSwingEnable;

     }

     private void SetSwingEnable(object sender, EventArgs e)
     {

          /*  StartCoroutine(SwingBall());*/


     }

     public void BounceUpdate()
     {
          PlayerPrefs.GetInt("BallBounceMoney", 100);


          PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money", 0) - PlayerPrefs.GetInt("BallBounceMoney", 100));
          Debug.Log((PlayerPrefs.GetInt("Money", 0)));
          if (PlayerPrefs.GetInt("Money", 0) > 0)

          {

               PlayerPrefs.GetInt("BallBounceLevel", 1);
               PlayerPrefs.SetFloat("BallBounce", PlayerPrefs.GetFloat("BallBounce") + 0.2f);

               Debug.Log(PlayerPrefs.GetFloat("BallBounce") + "Bounce Updated!");

               PlayerPrefs.SetInt("BallBounceLevel", PlayerPrefs.GetInt("BallBounceLevel", 1) + 1);

          }

          PlayerPrefs.SetInt("BallBounceMoney", PlayerPrefs.GetInt("BallBounceMoney", 100) + ballBounceMoneyRate);
     }

     public void RocketBooster()
     {

          Debug.Log("boost me");
          ball.GetComponent<Rigidbody>().AddForce(new Vector3(0, PlayerPrefs.GetInt("RocketBoosterEffect", 20) / 2, PlayerPrefs.GetInt("RocketBoosterEffect", 20)), ForceMode.Impulse);
          ball.GetComponent<Rigidbody>().angularDrag = 2;
          ball.GetComponent<Rigidbody>().angularVelocity = new Vector3(0f, 2f, 0f);


     }

     public void RocketBoosterUpdate()
     {

          PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money", 0) - 200);

          if (PlayerPrefs.GetInt("Money", 0) > 0)
          {

               PlayerPrefs.GetInt("RocketBoostLevel", 1);

               PlayerPrefs.GetInt("RocketBoosterEffect", 20);
               PlayerPrefs.SetInt("RocketBoosterEffect", PlayerPrefs.GetInt("RocketBoosterEffect", 0) + PlayerPrefs.GetInt("RocketBoosterEffect", 100));
               Debug.Log(PlayerPrefs.GetInt("RocketBoosterEffect", 20) + "Rocket Booster Updated!");
               PlayerPrefs.SetInt("RocketBoostLevel", PlayerPrefs.GetInt("RocketBoostLevel", 1) + 1);

          }
     }

     void SwingTapUpdate()
     {
          PlayerPrefs.GetInt("SwingTime", 2);

          PlayerPrefs.SetInt("SwingTime", PlayerPrefs.GetInt("SwingTime", 2) + PlayerPrefs.GetInt("SwingTime", 2));

     }
     public void StickPowerUpdate()
     {

          PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money", 0) - 200);

          if (PlayerPrefs.GetInt("Money", 0) > 0)
          {

               PlayerPrefs.GetInt("ClubPowerLevel", 1);
               
               PlayerPrefs.GetFloat("Power", 0.5f);
               PlayerPrefs.SetFloat("Power", PlayerPrefs.GetFloat("Power", 0.5f) + 0.2f);
               Debug.Log(PlayerPrefs.GetFloat("Power", 0.5f) + "Stick Power Updated!");
               PlayerPrefs.SetInt("ClubPowerLevel", PlayerPrefs.GetInt("ClubPowerLevel", 1) + 1);

          }

     }



     // Update is called once per frame
     void Update()
     {
          ClubPowerLevel.text = PlayerPrefs.GetInt("ClubPowerLevel", 1).ToString();
          RocketBoosterLevel.text = PlayerPrefs.GetInt("RocketBoostLevel", 1).ToString();
          BallBounceLevel.text = PlayerPrefs.GetInt("BallBounceLevel", 1).ToString();

     }

     IEnumerator SwingBall()
     {
          float swingTime = PlayerPrefs.GetInt("SwingTime", 2);
          Vector3 initialPosition = ball.transform.position;
          Vector3 targetPosition = initialPosition;

          while (swingTime > 0)
          {
               Vector2 swipeInput = StickController.instance.swipeInput.BallSwipe.JoyStick.ReadValue<Vector2>();


               targetPosition += new Vector3(1, ball.transform.position.y, ball.transform.position.z);


               ball.transform.position = Vector3.Lerp(ball.transform.position, targetPosition, Time.deltaTime);

               Debug.Log("move");


               swingTime -= Time.deltaTime;

               yield return null;
          }
     }

     public void DefaultStickPower()
     {
          PlayerPrefs.SetFloat("Power", 0.3f);
          PlayerPrefs.SetInt("ClubPowerLevel", 1);

     }

     public void DefaultMoney()
     {
          PlayerPrefs.SetFloat("Money", 0f);

     }

     public void DefaultBounceBall()
     {

          PlayerPrefs.SetFloat("BallBounce", 0.7f);
          PlayerPrefs.SetInt("BallBounceLevel", 1);
     }

     public void DefaultRocketBooster()
     {
          PlayerPrefs.SetInt("RocketBoosterEffect", 20);
          PlayerPrefs.SetInt("RocketBoostLevel", 1);

     }


     private void OnDisable()
     {
          StickController.OnBallLaunched -= SetSwingEnable;
     }



}
