using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    public TalkManager talkManager;
    public QuestManager questManager;

    public GameObject talkPanel;
    public GameObject scanObject;
    public bool isAction;
    public Text talkText;
    public int talkIndex;

    private void Start()
    {
        if (PlayerPrefs.GetInt("New") == 1)
        {
            StartCoroutine(StartTalk(1));
        }
        else 
        {
            talkPanel.SetActive(false);
        }
        PlayerPrefs.SetInt("New", 0);
    }
    public void Action(GameObject scanObj) 
    {
        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>();
        Talk(objData.id, objData.isNpc);

        talkPanel.SetActive(isAction);
    }

    void Talk(int id, bool isNpc) 
    {
        int questTalkIndex = questManager.GetQuestTalkIndex(id);

        //퀘스트 번호+npc id = 퀘스트 대화 데이터 id
        string talkData = talkManager.GetTalk(id+questTalkIndex, talkIndex);

        if (talkData == null) 
        {
            isAction = false;
            talkIndex = 0;
            questManager.CheckQuest(id);
            //Debug.Log(questManager.CheckQuest(id));
            return;
        }
        if (isNpc)
        {
            talkText.text = talkData;
        }
        else 
        {
            talkText.text = talkData;
        }

        isAction = true;
        talkIndex++;
    }

    IEnumerator StartTalk(int id)
    {
        int talkIndex = 0;
        //퀘스트 번호+npc id = 퀘스트 대화 데이터 id
        string talkData = talkManager.GetTalk(id, talkIndex);

        talkText.text = talkData;

        talkIndex++;
        yield return new WaitForSeconds(2f);
        talkData = talkManager.GetTalk(id, talkIndex);
        talkText.text = talkData;

        talkIndex++;
        yield return new WaitForSeconds(2f);
        talkData = talkManager.GetTalk(id, talkIndex);
        talkText.text = talkData;
        yield return new WaitForSeconds(2f);
        talkPanel.SetActive(false);

    }


}
