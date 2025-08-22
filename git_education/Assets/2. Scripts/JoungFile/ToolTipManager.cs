using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolTipManager : MonoBehaviour
{
    // [][0] : tool title, [][1] : git ��ɾ�, [][2] : git ��ɾ� ����
    public string[][] toolString = new string[][] {
        new string[]{"�� ����� ����" , "git init", "���ο� repository�� �����մϴ�!"},
        new string[]{"��� Ŀ�� �α� ����" , "git log", "���ݱ��� commit�� ������ ���ϴ�!"},
        new string[]{"���� ���� ����" , "git status", "branch�� �ִ� ���ϵ��� ���¸� �����ݴϴ�!"},
        new string[]{"���� �߰�" , "git add", "�� branch�� ������ �߰��մϴ�!"},
        new string[]{"���� ���� ����" , "git commit", "�� ����ҿ� ���� ��������� �����մϴ�!"},
        new string[]{"�귣ġ ���� Ȯ��" , "git branch", "���� �귣ġ�� ������� ��带 �����ݴϴ�!"},
        new string[]{"�� �귣ġ ����" , "create branch", "���ο� �귣ġ�� �����մϴ�."},
        new string[]{"�귣ġ �̵�" , "git checkout", "�̹� �����ϴ� �귣ġ�� �̵��մϴ�."},
        new string[]{"�귣ġ ��ġ��" , "git merge", "�� �귣ġ�� ���ϴ� �귣ġ�� ��Ĩ�ϴ�."},
        new string[]{"�귣ġ ����" , "delete branch", "�� �귣ġ�� �����մϴ�."},
        new string[]{"���� ����" , "create file", "���� ������ ���ο� ������ �߰��մϴ�."}
    };

    public string[][] toolTextString = new string[][] {
        new string[]{"�� ����� ����" , "git init", "���ο� repository�� �����մϴ�!"},
        new string[]{"��� Ŀ�� �α� ����" , "git log", "���ݱ��� commit�� ������ ���ϴ�!"},
        new string[]{"���� ���� ����" , "git status", "branch�� �ִ� ���ϵ��� ���¸� �����ݴϴ�!"},
        new string[]{"���� �߰�" , "git add <����>", "�� branch�� ������ �߰��մϴ�!"},
        new string[]{"���� ���� ����" , "git commit -m \"<�޼���>\"", "�� ����ҿ� ���� ��������� �����մϴ�!"},
        new string[]{"�귣ġ ���� Ȯ��" , "git branch", "���� �귣ġ�� ������� ��带 �����ݴϴ�!"},
        new string[]{"�� �귣ġ ����" , "git branch <���ο� �귣ġ>", "���ο� �귣ġ�� �����մϴ�."},
        new string[]{"�귣ġ �̵�" , "git checkout", "�̹� �����ϴ� �귣ġ�� �̵��մϴ�."},
        new string[]{"�귣ġ ��ġ��" , "git merge <branch>", "�� �귣ġ�� ���ϴ� �귣ġ�� ��Ĩ�ϴ�."},
        new string[]{"�귣ġ ����" , "git branch -D <������ �귣ġ>", "�� �귣ġ�� �����մϴ�."},
        new string[]{"���� ����" , "create file <����>", "���� ������ ���ο� ������ �߰��մϴ�."}
    };

    public GameObject toolSample;
    public GameObject toolContent;
    public int currentModeNum;

    private void Awake()
    {
        toolContent = GameObject.Find("ToolContent");
        currentModeNum = PlayerPrefs.GetInt("Mode");
    }

    private void Start()
    {
        InputToolData();
    }

    // Tip â�� ������Ʈ ��ȯ �Լ�    
    void InputToolData()
    {
        if (currentModeNum == 0)
        {
            for (int DataCount = 0; DataCount < toolString.Length; DataCount++)
            {
                GameObject currentData = Instantiate(toolSample, new Vector3(0, 0, 0), Quaternion.identity, toolContent.transform);
                currentData.transform.GetChild(0).GetComponent<TMP_Text>().text = (DataCount + 1) + ". " + toolString[DataCount][0];
                currentData.transform.GetChild(1).GetComponent<TMP_Text>().text = toolString[DataCount][1] + " - " + toolString[DataCount][2];
            }
        }

        else
        {
            for (int DataCount = 0; DataCount < toolTextString.Length; DataCount++)
            {
                GameObject currentData = Instantiate(toolSample, new Vector3(0, 0, 0), Quaternion.identity, toolContent.transform);
                currentData.transform.GetChild(0).GetComponent<TMP_Text>().text = (DataCount + 1) + ". " + toolTextString[DataCount][0];
                currentData.transform.GetChild(1).GetComponent<TMP_Text>().text = toolTextString[DataCount][1] + " - " + toolTextString[DataCount][2];
            }
        }
    }
}
