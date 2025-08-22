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
        questId = PlayerPrefs.GetInt("퀘스트");
    }

    void GenerateData() 
    {
        questList.Add(10, new QuestData("첫번째 업무", new int[] { 1000, 2000 }));

        questList.Add(20, new QuestData("두번째 업무", new int[] { 1000, 2000 }));

        questList.Add(30, new QuestData("세번째 업무", new int[] { 1000, 2000 }));

        questList.Add(40, new QuestData("네번째 업무", new int[] { 1000, 2000 }));

        questList.Add(50, new QuestData("다섯번째 업무", new int[] { 1000, 2000 }));
    }

    public int GetQuestTalkIndex(int id) 
    {
        return questId + questActionIndex; //퀘스트 번호+순서 인덱스 = 퀘스트 고유 번호
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
        PlayerPrefs.SetInt("퀘스트", questId);

        Debug.Log(questList[questId].questName);
        SceneManager.LoadScene("JoungTest");
        questId += 10;
        questActionIndex = 0;
    }

  
}
