using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public InGameManager GM;
    public FileManager FM;
    public TMP_Text fileName;
    public InputManager IM;

    public void Awake()
    {
        GM = GameObject.Find("GameManager").GetComponent<InGameManager>();
    }

    public void Update()
    {
        // 파일이나 브런치 커밋 입력하는 구간
        if (GM.fileNameInput.text.Length > 0 && Input.GetKeyDown(KeyCode.Return))
        {
            IM = GameObject.Find("GameManager").GetComponent<InputManager>();
            if (PlayerPrefs.GetInt("Mode") == 0) // button Mode 일때
            {
                if (InputManager.currentInputData == (int)InputManager.EInputState.CreateFile)
                {
                    IM.InputFileName();
                }

                else if (InputManager.currentInputData == (int)InputManager.EInputState.Commit)
                {
                    IM.InputVersionName();
                }

                else if (InputManager.currentInputData == (int)InputManager.EInputState.CreateBranch)
                {
                    IM.InputBranchName();
                }

                else if (InputManager.currentInputData == (int)InputManager.EInputState.Add)
                {
                    IM.InputAddFileName();
                }
            }

            else if(PlayerPrefs.GetInt("Mode") == 1) // Text Mode 일때
            {
                IM.InputCommandText();
            }
        }
    }

    // 브런치안에 있는 파일을 열고 안에 내용을 수정할 때 사용하는 함수
    public void fileContentInput()
    {
        GM = GameObject.Find("GameManager").GetComponent<InGameManager>();
        RectTransform fileInputPos = GM.fileInputPanel.GetComponent<RectTransform>();
        fileName = GameObject.Find("FileName").GetComponent<TMP_Text>();
        FM = GameObject.Find("GameManager").GetComponent<FileManager>();
        FM.fileName = fileName;

        string[] formatFile = EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<TMP_Text>().text.Split('-');
        fileName.text = formatFile[0];

        if (fileInputPos.anchoredPosition.x > 0)
        {
            int count;
            for (count = 0; count < FM.fileData[FM.currentFileBranch].Count; count++)
            {
                if (FM.fileData[FM.currentFileBranch][count].name == fileName.text)
                {
                    FM.inputTextBox.text = FM.fileData[FM.currentFileBranch][count].content;
                    break;
                }
            }
            fileInputPos.DOAnchorPosX(-700, 1f).SetEase(Ease.OutBounce);
        }
        else
        {
            fileInputPos.DOAnchorPosX(700, 1f).SetEase(Ease.OutBounce);
        }
    }

    // 경고창띄울때 사용하는 함수
    public void popControl()
    {
        if (GM.popPanel.activeSelf)
        {
            GM.popPanel.SetActive(false);
        }
    }

    public void ReStartGame()
    {
        SceneManager.LoadScene("JoungTest");
    }
}
