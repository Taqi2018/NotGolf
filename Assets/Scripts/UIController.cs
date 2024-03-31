using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

     public Text currencyText;
    public GameObject upgradePanel,ballPanel,teePanel,stickPanel;


     public void RestartGame()
     {
          SceneManager.LoadScene(SceneManager.GetActiveScene().name);
          Debug.Log("Hello");
     }


     private void Update()
     {
          currencyText.text = PlayerPrefs.GetInt("Money", 0).ToString();
     }

    public void OpenUpgradePanel()
    {
        upgradePanel.SetActive(true);
    }

    public void CloseUpgradePanel()
    {
        upgradePanel.SetActive(false);
    }

    public void OpenBallPanel()
    {
        ballPanel.SetActive(true);
    }
    public void CloseBallPanel()
    {
        ballPanel.SetActive(false);
    }

    public void OpenTeePanel()
    {
        teePanel.SetActive(true);
    }
    public void CloseTeePanel()
    {
        teePanel.SetActive(false);
    }
    public void OpenStickPanel()
    {
        stickPanel.SetActive(true);
    }
    public void CloseStickPanel()
    {
        stickPanel.SetActive(false);
    }

}
