using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MissionManager : MonoBehaviour
{
    [SerializeField]
    GameObject missionSample; // 미션 형식 프리팹 
    [SerializeField]
    List<GameObject> currentMission; // 지금 미션 리스트 저장

    static public int currentMissionNumber; // 미션 넘버 전달

    private int currentMissionCount = 0; // 지금 진행하고 있는 퀘스트 넘버 첫번째 퀘스트 = 10 이후 +10씩 증가
    Dictionary<int, string> missionData = new Dictionary<int, string>(); // 1001 ~ 100N 까지 미션 흐름도 저장

    [SerializeField]
    GameObject InGameMissionBackGround; // 미션을 넣는 Content 게임오브젝트 저장

    private void Awake()
    {
        InGameMissionBackGround = GameObject.Find("MissionContent");
        InputMissionData();
    }

    private void Start()
    {
        // 1 == 첫번째 퀘스트 이런식으로 받는다 생각
        currentMissionCount = PlayerPrefs.GetInt("퀘스트");
        currentMissionNumber = (currentMissionCount / 10) * 1000 + 1;
        //currentMissionNumber = testNum * 1000 + 1;
        InputPanelData();
    }

    private void Update()
    {
        // 미션 클리어 화면 테스트
        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentMissionNumber += 1;
            InputPanelData();
        }

        // 임의 퀘스트 설정
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayerPrefs.SetInt("퀘스트", 10);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayerPrefs.SetInt("퀘스트", 20);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlayerPrefs.SetInt("퀘스트", 30);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            PlayerPrefs.SetInt("퀘스트", 40);
        }
    }

    // 미션 내용 인풋 함수
    public void InputMissionData()
    {
        //missionData.Add(3001, "");
        // 첫번째 퀘스트 흐름도 : 1001 ~ 1005
        missionData.Add(1001, "저장소 Repository를 생성하세요!");
        missionData.Add(1002, "원하는 파일을 하나 만들어보세요!");
        missionData.Add(1003, "생성한 저장소에 있는 파일을 확인해보세요!");
        missionData.Add(1004, "저장소에 파일을 추가해주세요!");
        missionData.Add(1005, "저장소에 있는 파일을 다시 확인해보고 2단계와 비교해보세요!");
        missionData.Add(1006, "저장소에 추가한 파일을 확정시켜주세요!");
        missionData.Add(1007, "추가한 내용을 확인해보세요!");
        missionData.Add(1008, "Clear");
        // 두번째 퀘스트 흐름도
        missionData.Add(2001, "현재 존재하는 branch를 확인해보세요!");
        missionData.Add(2002, "새로운 테스트 branch를 생성하세요!");
        missionData.Add(2003, "현재 존재하는 branch를 다시 확인해보세요!");
        missionData.Add(2004, "현재 브런치에서 파일을 하나 만들어주세요!");
        missionData.Add(2005, "만든 파일을 저장소에 추가해주세요!");
        missionData.Add(2006, "파일 상태를 확인해주세요!");
        missionData.Add(2007, "저장소에 추가한 파일을 확정시켜주세요!");
        missionData.Add(2008, "새로 생성한 branch에서 변경한 내용을 메인 branch로 merge 해보세요!");
        missionData.Add(2009, "메인 branch의 내용이 잘 변경되었는지 확인해보고 새로 추가했던 branch를 삭제하세요!");
        missionData.Add(2010, "Clear");
        // 세번째 퀘스트 흐름도
        missionData.Add(3001, "박부장님의 test branch를 메인 branch로 merge 해보세요 !");
        missionData.Add(3002, "park 파일의 문제의 내용을 변경하세요.");
        missionData.Add(3003, "올바른 park 파일을 저장소에 merge를 성공하세요!");
        missionData.Add(3004, "merge에 성공했다면 test branch를 commit해보세요!");
        missionData.Add(3005, "Clear");
        // 네번째 퀘스트 흐름도
        missionData.Add(4001, "원격 저장소에 test branch 파일을 저장해주세요!");
        missionData.Add(4002, "새로운 new computer브런치에 원격저장소에 있는 파일을 가져오세요");
        missionData.Add(4003, "\"소감문\"파일에 후기를 작성해주세요");
        missionData.Add(4004, "\"소감문\"파일을 저장소에 저장해주세요");
        missionData.Add(4005, "원격저장소에 다시 push해 주세요!");
        missionData.Add(4006, "Clear");
    }

    // 현재 퀘스트 번호 받아 현재 진행도에 따라 퀘스트 판넬에 인풋
    public void InputPanelData()
    {
        int currentNum = 0;
        currentMission.Add(Instantiate(missionSample, new Vector3(0, 0, 0), Quaternion.identity, InGameMissionBackGround.transform));

        // currentNum 이 0이면 처음 퀘스트 인풋
        currentNum = currentMission.Count - 1;
        if (currentNum == 0)
        {
            currentMission[currentNum].transform.GetChild(0).GetComponent<TMP_Text>().text = "퀘스트 " + (currentMissionNumber % 1000);
            currentMission[currentNum].transform.GetChild(1).GetComponent<TMP_Text>().text = missionData[currentMissionNumber];
        }
        else
        {
            if(missionData[currentMissionNumber] == "Clear")
            {
                PlayerPrefs.SetInt("퀘스트", currentMissionCount + 10);
                SceneManager.LoadScene("chaetest");
            }

            currentMission[currentNum-1].transform.GetChild(0).GetComponent<TMP_Text>().text = "퀘스트 " + ((currentMissionNumber - 1) % 1000) + " 클리어!";
            currentMission[currentNum-1].transform.GetChild(1).GetComponent<TMP_Text>().text = missionData[currentMissionNumber - 1];
            currentMission[currentNum-1].transform.GetChild(1).GetComponent<TMP_Text>().fontStyle = FontStyles.Strikethrough;

            currentMission[currentNum].transform.GetChild(0).GetComponent<TMP_Text>().text = "퀘스트 " + (currentMissionNumber % 1000);
            currentMission[currentNum].transform.GetChild(1).GetComponent<TMP_Text>().text = missionData[currentMissionNumber];
        }
    }
}