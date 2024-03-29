using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

     public Text currencyText;
     public void RestartGame()
     {
          SceneManager.LoadScene(SceneManager.GetActiveScene().name);
          Debug.Log("Hello");
     }


     private void Update()
     {
          currencyText.text = PlayerPrefs.GetInt("Money", 0).ToString();
     }
}
