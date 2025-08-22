using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartButton : MonoBehaviour
{
    public GameObject modeSelectPanel;
    public GameObject mainPanel;
    public GameObject popPanel;
    
    public void ReLoadButtonChlick()
    {
        // 저장되지않았을 경우 출력
        if(PlayerPrefs.GetInt("퀘스트") == 10)
        {
            popPanel.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetInt("New", 0);
            mainPanel.SetActive(false);
            modeSelectPanel.SetActive(true);
        }
    }

    public void NewStartButtonChlick()
    {
        PlayerPrefs.SetInt("퀘스트", 10);
        PlayerPrefs.SetInt("New", 1);
        mainPanel.SetActive(false);
        modeSelectPanel.SetActive(true);
    }

    public void EasyModeButton()
    {
        PlayerPrefs.SetInt("Mode", 0);
        SceneManager.LoadScene("chaeTest");
    }

    public void HardModeButton()
    {
        PlayerPrefs.SetInt("Mode", 1);
        SceneManager.LoadScene("chaeTest");
    }

    public void PopTrigger()
    {
        if (popPanel.activeSelf)
        {
            popPanel.SetActive(false);
        }
    }
}
