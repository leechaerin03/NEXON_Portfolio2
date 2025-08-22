using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestManager : MonoBehaviour
{

    public int questId;
    public int questActionIndex;

    Dictionary<int, QuestData> questList;
    void Start()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
        questId = PlayerPrefs.GetInt("����Ʈ");
    }

    void GenerateData() 
    {
        questList.Add(10, new QuestData("ù��° ����", new int[] { 1000, 2000 }));

        questList.Add(20, new QuestData("�ι�° ����", new int[] { 1000, 2000 }));

        questList.Add(30, new QuestData("����° ����", new int[] { 1000, 2000 }));

        questList.Add(40, new QuestData("�׹�° ����", new int[] { 1000, 2000 }));

        questList.Add(50, new QuestData("�ټ���° ����", new int[] { 1000, 2000 }));
    }

    public int GetQuestTalkIndex(int id) 
    {
        return questId + questActionIndex; //����Ʈ ��ȣ+���� �ε��� = ����Ʈ ���� ��ȣ
    }

    public string CheckQuest(int id) 
    {
        if(id == questList[questId].npcId[questActionIndex])
            questActionIndex++;

        if (questActionIndex == questList[questId].npcId.Length)
            NextQuest();

        return questList[questId].questName;
    }

    void NextQuest() 
    {
        PlayerPrefs.SetInt("����Ʈ", questId);

        Debug.Log(questList[questId].questName);
        SceneManager.LoadScene("JoungTest");
        questId += 10;
        questActionIndex = 0;
    }

  
}
