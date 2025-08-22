using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MissionManager : MonoBehaviour
{
    [SerializeField]
    GameObject missionSample; // �̼� ���� ������ 
    [SerializeField]
    List<GameObject> currentMission; // ���� �̼� ����Ʈ ����

    static public int currentMissionNumber; // �̼� �ѹ� ����

    private int currentMissionCount = 0; // ���� �����ϰ� �ִ� ����Ʈ �ѹ� ù��° ����Ʈ = 10 ���� +10�� ����
    Dictionary<int, string> missionData = new Dictionary<int, string>(); // 1001 ~ 100N ���� �̼� �帧�� ����

    [SerializeField]
    GameObject InGameMissionBackGround; // �̼��� �ִ� Content ���ӿ�����Ʈ ����

    private void Awake()
    {
        InGameMissionBackGround = GameObject.Find("MissionContent");
        InputMissionData();
    }

    private void Start()
    {
        // 1 == ù��° ����Ʈ �̷������� �޴´� ����
        currentMissionCount = PlayerPrefs.GetInt("����Ʈ");
        currentMissionNumber = (currentMissionCount / 10) * 1000 + 1;
        //currentMissionNumber = testNum * 1000 + 1;
        InputPanelData();
    }

    private void Update()
    {
        // �̼� Ŭ���� ȭ�� �׽�Ʈ
        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentMissionNumber += 1;
            InputPanelData();
        }

        // ���� ����Ʈ ����
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayerPrefs.SetInt("����Ʈ", 10);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayerPrefs.SetInt("����Ʈ", 20);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlayerPrefs.SetInt("����Ʈ", 30);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            PlayerPrefs.SetInt("����Ʈ", 40);
        }
    }

    // �̼� ���� ��ǲ �Լ�
    public void InputMissionData()
    {
        //missionData.Add(3001, "");
        // ù��° ����Ʈ �帧�� : 1001 ~ 1005
        missionData.Add(1001, "����� Repository�� �����ϼ���!");
        missionData.Add(1002, "���ϴ� ������ �ϳ� ��������!");
        missionData.Add(1003, "������ ����ҿ� �ִ� ������ Ȯ���غ�����!");
        missionData.Add(1004, "����ҿ� ������ �߰����ּ���!");
        missionData.Add(1005, "����ҿ� �ִ� ������ �ٽ� Ȯ���غ��� 2�ܰ�� ���غ�����!");
        missionData.Add(1006, "����ҿ� �߰��� ������ Ȯ�������ּ���!");
        missionData.Add(1007, "�߰��� ������ Ȯ���غ�����!");
        missionData.Add(1008, "Clear");
        // �ι�° ����Ʈ �帧��
        missionData.Add(2001, "���� �����ϴ� branch�� Ȯ���غ�����!");
        missionData.Add(2002, "���ο� �׽�Ʈ branch�� �����ϼ���!");
        missionData.Add(2003, "���� �����ϴ� branch�� �ٽ� Ȯ���غ�����!");
        missionData.Add(2004, "���� �귱ġ���� ������ �ϳ� ������ּ���!");
        missionData.Add(2005, "���� ������ ����ҿ� �߰����ּ���!");
        missionData.Add(2006, "���� ���¸� Ȯ�����ּ���!");
        missionData.Add(2007, "����ҿ� �߰��� ������ Ȯ�������ּ���!");
        missionData.Add(2008, "���� ������ branch���� ������ ������ ���� branch�� merge �غ�����!");
        missionData.Add(2009, "���� branch�� ������ �� ����Ǿ����� Ȯ���غ��� ���� �߰��ߴ� branch�� �����ϼ���!");
        missionData.Add(2010, "Clear");
        // ����° ����Ʈ �帧��
        missionData.Add(3001, "�ں������ test branch�� ���� branch�� merge �غ����� !");
        missionData.Add(3002, "park ������ ������ ������ �����ϼ���.");
        missionData.Add(3003, "�ùٸ� park ������ ����ҿ� merge�� �����ϼ���!");
        missionData.Add(3004, "merge�� �����ߴٸ� test branch�� commit�غ�����!");
        missionData.Add(3005, "Clear");
        // �׹�° ����Ʈ �帧��
        missionData.Add(4001, "���� ����ҿ� test branch ������ �������ּ���!");
        missionData.Add(4002, "���ο� new computer�귱ġ�� ��������ҿ� �ִ� ������ ����������");
        missionData.Add(4003, "\"�Ұ���\"���Ͽ� �ı⸦ �ۼ����ּ���");
        missionData.Add(4004, "\"�Ұ���\"������ ����ҿ� �������ּ���");
        missionData.Add(4005, "��������ҿ� �ٽ� push�� �ּ���!");
        missionData.Add(4006, "Clear");
    }

    // ���� ����Ʈ ��ȣ �޾� ���� ���൵�� ���� ����Ʈ �ǳڿ� ��ǲ
    public void InputPanelData()
    {
        int currentNum = 0;
        currentMission.Add(Instantiate(missionSample, new Vector3(0, 0, 0), Quaternion.identity, InGameMissionBackGround.transform));

        // currentNum �� 0�̸� ó�� ����Ʈ ��ǲ
        currentNum = currentMission.Count - 1;
        if (currentNum == 0)
        {
            currentMission[currentNum].transform.GetChild(0).GetComponent<TMP_Text>().text = "����Ʈ " + (currentMissionNumber % 1000);
            currentMission[currentNum].transform.GetChild(1).GetComponent<TMP_Text>().text = missionData[currentMissionNumber];
        }
        else
        {
            if(missionData[currentMissionNumber] == "Clear")
            {
                PlayerPrefs.SetInt("����Ʈ", currentMissionCount + 10);
                SceneManager.LoadScene("chaetest");
            }

            currentMission[currentNum-1].transform.GetChild(0).GetComponent<TMP_Text>().text = "����Ʈ " + ((currentMissionNumber - 1) % 1000) + " Ŭ����!";
            currentMission[currentNum-1].transform.GetChild(1).GetComponent<TMP_Text>().text = missionData[currentMissionNumber - 1];
            currentMission[currentNum-1].transform.GetChild(1).GetComponent<TMP_Text>().fontStyle = FontStyles.Strikethrough;

            currentMission[currentNum].transform.GetChild(0).GetComponent<TMP_Text>().text = "����Ʈ " + (currentMissionNumber % 1000);
            currentMission[currentNum].transform.GetChild(1).GetComponent<TMP_Text>().text = missionData[currentMissionNumber];
        }
    }
}