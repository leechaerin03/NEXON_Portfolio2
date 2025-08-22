using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolTipManager : MonoBehaviour
{
    // [][0] : tool title, [][1] : git 명령어, [][2] : git 명령어 설명
    public string[][] toolString = new string[][] {
        new string[]{"깃 저장소 생성" , "git init", "새로운 repository를 생성합니다!"},
        new string[]{"모든 커밋 로그 보기" , "git log", "지금까지 commit한 정보를 봅니다!"},
        new string[]{"파일 상태 보기" , "git status", "branch에 있는 파일들의 상태를 보여줍니다!"},
        new string[]{"파일 추가" , "git add", "현 branch에 파일을 추가합니다!"},
        new string[]{"변경 사항 저장" , "git commit", "현 저장소에 파일 변경사항을 저장합니다!"},
        new string[]{"브랜치 상태 확인" , "git branch", "현재 브랜치로 만들어진 노드를 보여줍니다!"},
        new string[]{"새 브랜치 생성" , "create branch", "새로운 브랜치를 생성합니다."},
        new string[]{"브랜치 이동" , "git checkout", "이미 존재하는 브랜치로 이동합니다."},
        new string[]{"브랜치 합치기" , "git merge", "현 브랜치와 원하는 브랜치를 합칩니다."},
        new string[]{"브랜치 삭제" , "delete branch", "현 브랜치를 삭제합니다."},
        new string[]{"파일 생성" , "create file", "현재 버전에 새로운 파일을 추가합니다."}
    };

    public string[][] toolTextString = new string[][] {
        new string[]{"깃 저장소 생성" , "git init", "새로운 repository를 생성합니다!"},
        new string[]{"모든 커밋 로그 보기" , "git log", "지금까지 commit한 정보를 봅니다!"},
        new string[]{"파일 상태 보기" , "git status", "branch에 있는 파일들의 상태를 보여줍니다!"},
        new string[]{"파일 추가" , "git add <파일>", "현 branch에 파일을 추가합니다!"},
        new string[]{"변경 사항 저장" , "git commit -m \"<메세지>\"", "현 저장소에 파일 변경사항을 저장합니다!"},
        new string[]{"브랜치 상태 확인" , "git branch", "현재 브랜치로 만들어진 노드를 보여줍니다!"},
        new string[]{"새 브랜치 생성" , "git branch <새로운 브랜치>", "새로운 브랜치를 생성합니다."},
        new string[]{"브랜치 이동" , "git checkout", "이미 존재하는 브랜치로 이동합니다."},
        new string[]{"브랜치 합치기" , "git merge <branch>", "현 브랜치와 원하는 브랜치를 합칩니다."},
        new string[]{"브랜치 삭제" , "git branch -D <삭제할 브랜치>", "현 브랜치를 삭제합니다."},
        new string[]{"파일 생성" , "create file <파일>", "현재 버전에 새로운 파일을 추가합니다."}
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

    // Tip 창에 오브젝트 소환 함수    
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
