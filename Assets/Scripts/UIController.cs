using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
     public void RestartGame()
     {
          SceneManager.LoadScene("SmallCity3");
     }
}
