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
        // �����̳� �귱ġ Ŀ�� �Է��ϴ� ����
        if (GM.fileNameInput.text.Length > 0 && Input.GetKeyDown(KeyCode.Return))
        {
            IM = GameObject.Find("GameManager").GetComponent<InputManager>();
            if (PlayerPrefs.GetInt("Mode") == 0) // button Mode �϶�
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

            else if(PlayerPrefs.GetInt("Mode") == 1) // Text Mode �϶�
            {
                IM.InputCommandText();
            }
        }
    }

    // �귱ġ�ȿ� �ִ� ������ ���� �ȿ� ������ ������ �� ����ϴ� �Լ�
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

    // ���â��ﶧ ����ϴ� �Լ�
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
