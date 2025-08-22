using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InputManager : MonoBehaviour
{
    private bool canPark = false;
    //------------------------- file name input values -------------------------//
    private string inputName = null;

    public InGameManager GM;
    public FileManager FM;
    public MissionManager MM;

    [SerializeField]
    GameObject[] userButtons;

    // string = 명령어, enum = 순서 int로 전달
    Dictionary<string, int> userInputData = new Dictionary<string, int>()
    {
        { "git init", 0 },
        { "git log", 1 },
        { "git status", 2 },
        { "git add", 3 },
        { "git commit", 4 },
        { "create file", 5 },
        { "git pull", 6 },
        { "git branch", 7 },
        { "git branch create", 8 },
        { "git checkout", 9 },
        { "git merge", 10 },
        { "git branch -d", 11 },
        { "git push", 12 },
    };

    // git 명령어 사용 순서 0 ~ N
    public enum EInputState
    {
        Init,
        Log,
        Status,
        Add,
        Commit,
        CreateFile,
        Pull,
        Branch,
        CreateBranch,
        MoveBranch,
        Merge,
        DeleteBranch,
        Push,
    }

    static public int currentInputData = -1;

    private void Awake()
    {
        GM = GameObject.Find("GameManager").GetComponent<InGameManager>();
        FM = GameObject.Find("GameManager").GetComponent<FileManager>();
        MM = GameObject.Find("GameManager").GetComponent<MissionManager>();
    }
    public TreeMaker TM;
    private void Start()
    {
        GameStartSetting();
    }

    public void GameStartSetting()
    {
        Debug.Log("" + PlayerPrefs.GetInt("퀘스트")+ " : " +PlayerPrefs.GetInt("Mode"));
        if (PlayerPrefs.GetInt("Mode") == 1)
        {
            GM.userInput[2].transform.GetChild(0).GetComponent<TMP_Text>().text = "명령어를 입력해주세요";
            GM.userInput[1].SetActive(false);
            GM.userInput[2].SetActive(true);
        }
        MakeSetButton();

        if (PlayerPrefs.GetInt("퀘스트") == 20)
        {
            if (PlayerPrefs.GetInt("Mode") == 1)
            {
                StateCheck(0); // git init 실행
                GM.currentBranch = "Master Branch";
                StateCheck(CompareCommandText("create file park"));
                FM.fileData[GM.currentBranch][0].State = FileSample.EState.staged;
                StateCheck(CompareCommandText("git commit -m Version1"));
            }

            else if (PlayerPrefs.GetInt("Mode") == 0)
            {
                PlayerPrefs.SetInt("Mode", 1);
                StateCheck(0); // git init 실행
                GM.currentBranch = "Master Branch";
                StateCheck(CompareCommandText("create file park"));
                FM.fileData[GM.currentBranch][0].State = FileSample.EState.staged;
                StateCheck(CompareCommandText("git commit -m Version1"));
                PlayerPrefs.SetInt("Mode", 0);
            }
            //InGameManager.useInit = true;
        }

        if (PlayerPrefs.GetInt("퀘스트") == 30)
        {
            if (PlayerPrefs.GetInt("Mode") == 1)
            {
                TM = GameObject.Find("treemaker").GetComponent<TreeMaker>();
                StateCheck(0);
                GM.currentBranch = "Master Branch";
                TM.mergtmp = TM.click_obj;
                TM.click_obj = GameObject.Find(GM.currentBranch);
                StateCheck(CompareCommandText("create file park"));
                FM.fileData[GM.currentBranch][0].content = "start table" + "\n" + "salary : 10000$" + "\n" + "end table";
                StateCheck(CompareCommandText("create file joung"));
                StateCheck(CompareCommandText("create file chae"));
                StateCheck(CompareCommandText("create file hanyu"));
                for (int count = 0; count < FM.fileData[GM.currentBranch].Count; count++)
                {
                    FM.fileData[GM.currentBranch][count].State = FileSample.EState.staged;
                }
                StateCheck(CompareCommandText("git commit -m Version1"));
                GM.currentBranch = "Version1";
                TM.mergtmp = TM.click_obj;
                TM.click_obj = GameObject.Find(GM.currentBranch);
                StateCheck(CompareCommandText("git branch test branch"));
                GM.currentBranch = "test branch";
                FM.fileData[GM.currentBranch][0].content = "start table" + "\n" + "salary : 100000000$" + "\n" + "end table";
                StateCheck(CompareCommandText("create file totalFile"));
                FM.fileData[GM.currentBranch][FM.fileData[GM.currentBranch].Count - 1].content = "우리팀은 1등할 것이다 늘 그랬듯이";
                FM.fileData[GM.currentBranch][FM.fileData[GM.currentBranch].Count - 1].State = FileSample.EState.staged;
                StateCheck(CompareCommandText("git branch"));
            }

            else if(PlayerPrefs.GetInt("Mode") == 0)
            {
                PlayerPrefs.SetInt("Mode", 1);
                TM = GameObject.Find("treemaker").GetComponent<TreeMaker>();
                StateCheck(0);
                GM.currentBranch = "Master Branch";
                TM.mergtmp = TM.click_obj;
                TM.click_obj = GameObject.Find(GM.currentBranch);
                StateCheck(CompareCommandText("create file park"));
                FM.fileData[GM.currentBranch][0].content = "start table" + "\n" + "salary : 10000$" + "\n" + "end table";
                StateCheck(CompareCommandText("create file joung"));
                StateCheck(CompareCommandText("create file chae"));
                StateCheck(CompareCommandText("create file hanyu"));
                for (int count = 0; count < FM.fileData[GM.currentBranch].Count; count++)
                {
                    FM.fileData[GM.currentBranch][count].State = FileSample.EState.staged;
                }
                StateCheck(CompareCommandText("git commit -m Version1"));
                GM.currentBranch = "Version1";
                TM.mergtmp = TM.click_obj;
                TM.click_obj = GameObject.Find(GM.currentBranch);
                StateCheck(CompareCommandText("git branch test branch"));
                GM.currentBranch = "test branch";
                FM.fileData[GM.currentBranch][0].content = "start table" + "\n" + "salary : 100000000$" + "\n" + "end table";
                StateCheck(CompareCommandText("create file totalFile"));
                FM.fileData[GM.currentBranch][FM.fileData[GM.currentBranch].Count - 1].content = "우리팀은 1등할 것이다 늘 그랬듯이";
                FM.fileData[GM.currentBranch][FM.fileData[GM.currentBranch].Count - 1].State = FileSample.EState.staged;
                StateCheck(CompareCommandText("git branch"));
                PlayerPrefs.SetInt("Mode", 0);
            }
        }

        if (PlayerPrefs.GetInt("퀘스트") == 40)
        {
            if (PlayerPrefs.GetInt("Mode") == 1)
            {
                TM = GameObject.Find("treemaker").GetComponent<TreeMaker>();
                StateCheck(0);
                GM.currentBranch = "Master Branch";
                TM.mergtmp = TM.click_obj;
                TM.click_obj = GameObject.Find(GM.currentBranch);
                StateCheck(CompareCommandText("create file joung"));
                StateCheck(CompareCommandText("create file chae"));
                StateCheck(CompareCommandText("create file hanyu"));
                for (int count = 0; count < FM.fileData[GM.currentBranch].Count; count++)
                {
                    FM.fileData[GM.currentBranch][count].State = FileSample.EState.staged;
                }
                StateCheck(CompareCommandText("git commit -m Version1"));
                GM.currentBranch = "Version1";
                TM.mergtmp = TM.click_obj;
                TM.click_obj = GameObject.Find(GM.currentBranch);

                StateCheck(CompareCommandText("git branch test branch"));
                GM.currentBranch = "test branch";
                StateCheck(CompareCommandText("create file 소감문"));
                FM.fileData[GM.currentBranch][FM.fileData[GM.currentBranch].Count - 1].State = FileSample.EState.staged;

                GM.currentBranch = "Version1";
                TM.mergtmp = TM.click_obj;
                TM.click_obj = GameObject.Find(GM.currentBranch);
                StateCheck(CompareCommandText("git branch new computer"));

                StateCheck(CompareCommandText("git branch"));
            }

            else if (PlayerPrefs.GetInt("Mode") == 0)
            {
                PlayerPrefs.SetInt("Mode", 1);
                TM = GameObject.Find("treemaker").GetComponent<TreeMaker>();
                StateCheck(0);
                GM.currentBranch = "Master Branch";
                TM.mergtmp = TM.click_obj;
                TM.click_obj = GameObject.Find(GM.currentBranch);
                StateCheck(CompareCommandText("create file joung"));
                StateCheck(CompareCommandText("create file chae"));
                StateCheck(CompareCommandText("create file hanyu"));
                for (int count = 0; count < FM.fileData[GM.currentBranch].Count; count++)
                {
                    FM.fileData[GM.currentBranch][count].State = FileSample.EState.staged;
                }
                StateCheck(CompareCommandText("git commit -m Version1"));
                GM.currentBranch = "Version1";
                TM.mergtmp = TM.click_obj;
                TM.click_obj = GameObject.Find(GM.currentBranch);

                StateCheck(CompareCommandText("git branch test branch"));
                GM.currentBranch = "test branch";
                StateCheck(CompareCommandText("create file 소감문"));
                FM.fileData[GM.currentBranch][FM.fileData[GM.currentBranch].Count - 1].State = FileSample.EState.staged;

                GM.currentBranch = "Version1";
                TM.mergtmp = TM.click_obj;
                TM.click_obj = GameObject.Find(GM.currentBranch);
                StateCheck(CompareCommandText("git branch new computer"));

                StateCheck(CompareCommandText("git branch"));
                PlayerPrefs.SetInt("Mode", 0);
            }
        }
    }

    //---------------------------- button Mode Start -----------------------------//
    // 브런치안에 파일 생성하는 함수
    public void InputFileName()
    {
        inputName = GM.fileNameInput.text;
        FM.InputFileData(inputName);
        GM.fileNameInput.text = "";
        inputName = "";
        FM.filePanelSetting();
        if (MissionManager.currentMissionNumber == 1002 || MissionManager.currentMissionNumber == 2004)
        {
            MissionManager.currentMissionNumber += 1;
            MM.InputPanelData();
        }

        GM.userInput[2].SetActive(false);
        GM.userInput[1].SetActive(true);
    }

    // 브런치에서 원격저장소로 커밋할 때 이름 넣는 함수
    public void InputVersionName()
    {
        inputName = GM.fileNameInput.text;
        GM.fileNameInput.text = "";
        cb = GameObject.Find("treemaker").GetComponent<nuttoncommit>();
        cb.buttons(inputName);

        if (MissionManager.currentMissionNumber == 1006 || MissionManager.currentMissionNumber == 2007)
        {
            MissionManager.currentMissionNumber += 1;
            MM.InputPanelData();
        }

        if (GM.currentBranch == "test branch" && MissionManager.currentMissionNumber == 3004)
        {
            MissionManager.currentMissionNumber += 1;
            MM.InputPanelData();
        }

        inputName = ""; // 이름 초기화
        GM.userInput[2].SetActive(false);
        GM.userInput[1].SetActive(true);
    }

    // 브런치 생성할때 이름 설정하는 함수
    public void InputBranchName()
    {
        TM = GameObject.Find("treemaker").GetComponent<TreeMaker>();
        if (MissionManager.currentMissionNumber == 2002)
        {
            MissionManager.currentMissionNumber += 1;
            MM.InputPanelData();
        }
        inputName = GM.fileNameInput.text;
        GM.fileNameInput.text = "";
        bb = GameObject.Find("treemaker").GetComponent<nutton>();
        bb.buttons(inputName);
        inputName = ""; //이름 초기화
        TM.branmode = false;
        GM.userInput[2].SetActive(false);
        GM.userInput[1].SetActive(true);
    }

    // 원하는 파일을 커밋시킬때 입력하는 함수
    public void InputAddFileName()
    {
        int count = 0;
        Debug.Log("Start Add Input Name");
        inputName = GM.fileNameInput.text;
        GM.fileNameInput.text = "";

        for (count = 0; count < FM.fileData[GM.currentBranch].Count; count++)
        {
            if (FM.fileData[GM.currentBranch][count].name == inputName)
            {
                FM.fileData[GM.currentBranch][count].State = FileSample.EState.staged;
                if (InGameManager.useStatus)
                {
                    InGameManager.useStatus = false;
                    FM.setStatus();
                }

                if (MissionManager.currentMissionNumber == 1004 || MissionManager.currentMissionNumber == 2005)
                {
                    MissionManager.currentMissionNumber += 1;
                    MM.InputPanelData();
                }

                if (MissionManager.currentMissionNumber == 4004 && inputName == "소감문")
                {
                    MissionManager.currentMissionNumber += 1;
                    MM.InputPanelData();
                }

                GM.userInput[2].SetActive(false);
                GM.userInput[1].SetActive(true);
                break;
            }
        }

        if (count == FM.fileData[GM.currentBranch].Count)
        {
            GM.popPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = inputName + " - 해당 파일을 찾을 수 없습니다.";
            GM.popPanel.SetActive(true);
            GM.userInput[2].SetActive(false);
            GM.userInput[1].SetActive(true);
        }

        inputName = ""; //이름 초기화
    }

    public bool CanMerge(string toNodeString, string fromNodeString)
    {
        string findStr = "salary : ";
        for (int toCount = 0; toCount < FM.fileData[toNodeString].Count; toCount++)
        {
            for (int fromCount = 0; fromCount < FM.fileData[fromNodeString].Count; fromCount++)
            {
                if(FM.fileData[fromNodeString][fromCount].State == FileSample.EState.staged && FM.fileData[fromNodeString][fromCount].name == FM.fileData[toNodeString][toCount].name)
                {
                    string toContent = FM.fileData[toNodeString][toCount].content;
                    string fromContent = FM.fileData[fromNodeString][fromCount].content;

                    if (fromContent.Contains(findStr) && fromContent.Contains(findStr))
                    {
                        int toF = fromContent.IndexOf('$');
                        int toS = fromContent.IndexOf(findStr);
                        string fromSub = fromContent.Substring(toS, toF-toS);
                        toF = toContent.IndexOf('$');
                        toS = toContent.IndexOf(findStr);
                        string toSub = toContent.Substring(toS, toF-toS);   
                        if (fromSub != toSub)
                        {
                            return false;
                        }

                        else
                        {
                            if (FM.fileData[toNodeString][toCount].name == "park")
                            {
                                if (MissionManager.currentMissionNumber == 3003)
                                {
                                    MissionManager.currentMissionNumber += 1;
                                    MM.InputPanelData();
                                }
                            }
                            return true;
                        }
                    }
                }
            }
        }
        return true;
    }

    // 사용자 인풋 버튼 생성
    public void MakeSetButton()
    {
        for(int i = 0; i < userButtons.Length; i++)
        {
            Instantiate(userButtons[i], new Vector3(0, 0, 0), Quaternion.identity, GM.userInput[1].transform);
        }
    }

    // 인풋 버튼들 중 아무거나 눌렀을 때 실행하는 공통 함수
    public void ChlickButton()
    {
        GameObject currentButton = EventSystem.current.currentSelectedGameObject;
        GM = GameObject.Find("GameManager").GetComponent<InGameManager>(); // 새롭게 만들어진 버튼에다가 만들어줌
        FM = GameObject.Find("GameManager").GetComponent<FileManager>();
        string currentText = currentButton.transform.GetChild(0).name;

        if (userInputData.ContainsKey(currentText))
        {
            if(userInputData[currentText] == 0)
            {
                StateCheck(userInputData[currentText]);
            }
            else if(userInputData[currentText] != 0 && !InGameManager.useInit)
            {
                GM.popPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = "Git init을 먼저 해주세요!!";
                GM.popPanel.SetActive(true);
            }
            else
            {
                StateCheck(userInputData[currentText]);
            }
        }

        else
        {
            Debug.LogError("not found data : " + currentText);
        }
    }

    public nuttoncommit cb;
    public nutton bb;
    public deleteb db;
    public mergbutt mb;

    public void StateCheck(int currentState)
    {
        TM = GameObject.Find("treemaker").GetComponent<TreeMaker>();
        MM = GameObject.Find("GameManager").GetComponent<MissionManager>();
        EInputState StateName = (EInputState)currentState;
        int count;
        switch (StateName)
        {
            case EInputState.Init:
                if(MissionManager.currentMissionNumber == 1001)
                {
                    MissionManager.currentMissionNumber += 1;
                    MM.InputPanelData();
                }

                if (InGameManager.useInit)  
                {
                    GM.popPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = "저장소가 이미 존재합니다.";
                }
                else
                {
                    bb = GameObject.Find("treemaker").GetComponent<nutton>();
                    bb.buttons("Master Branch");
                    GM.popPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = "새로운 저장소를 생성합니다.";
                    InGameManager.useInit = true;
                    TM.click_obj = GameObject.Find("Master Branch");
                    GM.currentBranch = "Master Branch";
                }
                GM.popPanel.SetActive(true);
                break;

            case EInputState.Log:
                if (TM.logmode)
                    TM.logmode = false;
                else
                {
                    if (MissionManager.currentMissionNumber == 1007)
                    {
                        MissionManager.currentMissionNumber += 1;
                        MM.InputPanelData();
                    }
                    TM.logmode = true;
                }
                break;

            case EInputState.Status:
                if (InGameManager.useStatus)
                    InGameManager.useStatus = false;
                else
                    InGameManager.useStatus = true;
                FM.setStatus();
                break;

            case EInputState.Add:
                if (PlayerPrefs.GetInt("Mode") == 0 && FM.fileData.ContainsKey(GM.currentBranch))
                {
                    GM.userInput[2].transform.GetChild(0).GetComponent<TMP_Text>().text = "현재 브런치에 추가할 파일 이름을 적어주세요";
                    currentInputData = (int)EInputState.Add;
                    GM.userInput[1].SetActive(false);
                    GM.userInput[2].SetActive(true);
                }

                else if(PlayerPrefs.GetInt("Mode") == 1 && FM.fileData.ContainsKey(GM.currentBranch))
                {
                    for (count = 0; count < FM.fileData[GM.currentBranch].Count; count++)
                    {
                        if (FM.fileData[GM.currentBranch][count].name == nameAll)
                        {
                            FM.fileData[GM.currentBranch][count].State = FileSample.EState.staged;
                            if (InGameManager.useStatus)
                            {
                                InGameManager.useStatus = false;
                                FM.setStatus();
                            }

                            if (MissionManager.currentMissionNumber == 1004 || MissionManager.currentMissionNumber == 2005)
                            {
                                MissionManager.currentMissionNumber += 1;
                                MM.InputPanelData();
                            }

                            if (MissionManager.currentMissionNumber == 4004 && nameAll == "소감문")
                            {
                                MissionManager.currentMissionNumber += 1;
                                MM.InputPanelData();
                            }
                            break;
                        }
                    }

                    if (count == FM.fileData[GM.currentBranch].Count)
                    {
                        GM.popPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = inputName + " - 해당 파일을 찾을 수 없습니다.";
                        GM.popPanel.SetActive(true);
                    }

                    nameAll = ""; //이름 초기화
                }

                else
                {
                    GM.popPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = "현재 브런치에 파일이 존재하지 않습니다.";
                    GM.popPanel.SetActive(true);
                }
                break;

            case EInputState.Commit:
                if (!TM.click_obj)
                {
                    GM.popPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = "브런치가 존재하지 않습니다.";
                    GM.popPanel.SetActive(true);
                    break;
                }
                currentInputData = (int)EInputState.Commit;
                if (PlayerPrefs.GetInt("Mode") == 0)
                {
                    GM.userInput[2].transform.GetChild(0).GetComponent<TMP_Text>().text = "버전 이름을 입력해주세요";
                    GM.userInput[1].SetActive(false);
                    GM.userInput[2].SetActive(true);
                }

                else if(PlayerPrefs.GetInt("Mode") == 1)
                {
                    cb = GameObject.Find("treemaker").GetComponent<nuttoncommit>();
                    cb.buttons(nameAll);
                    nameAll = "";

                    if(GM.currentBranch == "test branch" && MissionManager.currentMissionNumber == 3004)
                    {
                        MissionManager.currentMissionNumber += 1;
                        MM.InputPanelData();
                    }

                    if (MissionManager.currentMissionNumber == 1006 || MissionManager.currentMissionNumber == 2007)
                    {
                        MissionManager.currentMissionNumber += 1;
                        MM.InputPanelData();
                    }
                }

                break;

            case EInputState.CreateFile:
                currentInputData = (int)EInputState.CreateFile;
                if (PlayerPrefs.GetInt("Mode") == 0)
                {
                    GM.userInput[2].transform.GetChild(0).GetComponent<TMP_Text>().text = "파일 이름을 입력해주세요";
                    GM.userInput[1].SetActive(false);
                    GM.userInput[2].SetActive(true);
                }

                else if(PlayerPrefs.GetInt("Mode") == 1)
                {
                    FM.InputFileData(nameAll);
                    nameAll = "";
                    FM.filePanelSetting();

                    if (MissionManager.currentMissionNumber == 1002 || MissionManager.currentMissionNumber == 2004)
                    {
                        MissionManager.currentMissionNumber += 1;
                        MM.InputPanelData();
                    }
                }
                break;

            case EInputState.Pull:
                if (!FM.fileData.ContainsKey("Master"))
                {
                    GM.popPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = "원격 레포지토리에 파일이 존재하지않습니다.";
                    GM.popPanel.SetActive(true);
                }

                else
                {
                    if (MissionManager.currentMissionNumber == 4002 && GM.currentBranch == "new computer")
                    {
                        MissionManager.currentMissionNumber += 1;
                        MM.InputPanelData();
                    }
                    for (int fromCount = 0; fromCount < FM.fileData["Master"].Count; ++fromCount)
                    {
                        int toCount;
                        for (toCount = 0; toCount < FM.fileData[GM.currentBranch].Count; ++toCount)
                        {
                            if (FM.fileData[GM.currentBranch][toCount].name == FM.fileData["Master"][fromCount].name)
                            {
                                FM.fileData[GM.currentBranch][toCount].content = FM.fileData["Master"][fromCount].content;
                                break;
                            }
                        }

                        if(toCount == FM.fileData[GM.currentBranch].Count)
                        {
                            FileSample tmpFile = new FileSample(FM.fileData["Master"][fromCount].name);
                            tmpFile.content = FM.fileData["Master"][fromCount].content;
                            tmpFile.State = FileSample.EState.staged;
                            FM.fileData[GM.currentBranch].Add(tmpFile);
                        }
                    }
                    FM.FilePanelSettingPR();
                }
                break;  

            case EInputState.Branch:
                if (TM.branmode)
                {
                    TM.branmode = false;
                }
                else
                {
                    if (MissionManager.currentMissionNumber == 2001 || MissionManager.currentMissionNumber == 2003)
                    {
                        MissionManager.currentMissionNumber += 1;
                        MM.InputPanelData();
                    }

                    TM.branmode = true;
                }
                break;

            case EInputState.CreateBranch:
                currentInputData = (int)EInputState.CreateBranch;
                if (PlayerPrefs.GetInt("Mode") == 0)
                {
                    GM.userInput[2].transform.GetChild(0).GetComponent<TMP_Text>().text = "브랜치 이름을 입력해주세요";
                    GM.userInput[1].SetActive(false);
                    GM.userInput[2].SetActive(true);
                }

                else if(PlayerPrefs.GetInt("Mode") == 1)
                {
                    if (MissionManager.currentMissionNumber == 2002)
                    {
                        MissionManager.currentMissionNumber += 1;
                        MM.InputPanelData();
                    }
                    bb = GameObject.Find("treemaker").GetComponent<nutton>();
                    bb.buttons(nameAll);
                    nameAll = "";
                    TM.branmode = false;
                }
                break;

            case EInputState.MoveBranch:
                break;

            case EInputState.Merge:
                if (PlayerPrefs.GetInt("Mode") == 0)
                {
                    TM.mergmode = true;

                }
                else if(PlayerPrefs.GetInt("Mode") == 1) // Text 형식 merge
                {
                    if (FM.fileData.ContainsKey(nameAll))
                    {
                        if (CanMerge(GM.currentBranch, nameAll))
                        {
                            if (MissionManager.currentMissionNumber == 2008)
                            {
                                MissionManager.currentMissionNumber += 1;
                                MM.InputPanelData();
                            }
                            mb = GameObject.Find("treemaker").GetComponent<mergbutt>();
                            mb.buttons();
                            TM.Merge(nameAll);
                            nameAll = "";
                        }

                        else
                        {
                            if (MissionManager.currentMissionNumber == 3001)
                            {
                                MissionManager.currentMissionNumber += 1;
                                MM.InputPanelData();
                            }
                            GM.popPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = "현재 파일을 merge할 수 없습니다. park 파일의 salary값을 확인해주세요";
                            GM.popPanel.SetActive(true);
                        }
                    }
                    else
                    {
                        GM.popPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = "브런치가 존재하지않습니다.";
                        GM.popPanel.SetActive(true);
                    }
                }
                break;

            case EInputState.DeleteBranch:
                if (PlayerPrefs.GetInt("Mode") == 0)
                {
                    if (MissionManager.currentMissionNumber == 2009)
                    {
                        MissionManager.currentMissionNumber += 1;
                        MM.InputPanelData();
                    }
                    db = GameObject.Find("treemaker").GetComponent<deleteb>();
                    db.buttons();
                }
                else // Text 형식 branchDelete
                {
                    if (FM.fileData.ContainsKey(nameAll)) // branch 삭제 부분
                    {
                        if (MissionManager.currentMissionNumber == 2009)
                        {
                            MissionManager.currentMissionNumber += 1;
                            MM.InputPanelData();
                        }
                        db = GameObject.Find("treemaker").GetComponent<deleteb>();
                        db.buttons(nameAll);
                        nameAll = "";
                    }
                    else
                    {
                        GM.popPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = "브런치가 존재하지않습니다.";
                        GM.popPanel.SetActive(true);
                    }
                }
                break;

            case EInputState.Push:
                for(count = 0; count < FM.fileData[GM.currentBranch].Count; ++count)
                {
                    if(FM.fileData[GM.currentBranch][count].State != FileSample.EState.staged)
                    {
                        GM.popPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = "commit을 먼저 해주세요!";
                        GM.popPanel.SetActive(true);
                        break;
                    }
                }

                if(count == FM.fileData[GM.currentBranch].Count)
                {
                    int toCount;
                    if (MissionManager.currentMissionNumber == 4001 && GM.currentBranch == "test branch")
                    {
                        MissionManager.currentMissionNumber += 1;
                        MM.InputPanelData();
                    }

                    if (MissionManager.currentMissionNumber == 4005 && GM.currentBranch == "new computer")
                    {
                        MissionManager.currentMissionNumber += 1;
                        MM.InputPanelData();
                    }

                    if (FM.fileData.ContainsKey("Master")) // 이미 한번 push를 한 상태일때
                    {
                        // 파일 한개씩 비교하면서 이미 있는 파일인지 아닌지 확인 후 인풋
                        for(int fromCount = 0; fromCount < FM.fileData[GM.currentBranch].Count; fromCount++)
                        {
                            for (toCount = 0; toCount < FM.fileData["Master"].Count; ++toCount)
                            {
                                if(FM.fileData["Master"][toCount].name == FM.fileData[GM.currentBranch][fromCount].name) // 같은 파일이 있을 경우
                                {
                                    FM.fileData["Master"][toCount].content = FM.fileData[GM.currentBranch][fromCount].content; // 문제 없을 경우 merge
                                }
                            }

                            if(toCount == FM.fileData["Master"].Count) // Master PR 안에 push할 파일이 없는 경우
                            {
                                FileSample tmpFile = new FileSample(FM.fileData[GM.currentBranch][fromCount].name);
                                tmpFile.content = FM.fileData[GM.currentBranch][fromCount].content;
                                tmpFile.State = FileSample.EState.staged;
                                FM.fileData["Master"].Add(tmpFile);
                            }
                        }
                    }
                    else // PR 안에 아무것도 존재 하지 않을 경우 git push -u origin Master 와 같은 역할
                    {
                        FM.fileData.Add("Master", new List<FileSample>());

                        for (count = 0; count < FM.fileData[GM.currentBranch].Count; ++count)
                        {
                            FM.fileData["Master"].Add(new FileSample(FM.fileData[GM.currentBranch][count].name));
                            FM.fileData["Master"][count].content = FM.fileData[GM.currentBranch][count].content;
                            FM.fileData["Master"][count].State = FileSample.EState.staged;
                        }
                    }
                    FM.PRPanelSetting();
                }
                break;
        }
    }
    //---------------------------- button Mode End -----------------------------//

    public void InputCommandText()
    {
        inputName = GM.fileNameInput.text;
        GM.fileNameInput.text = "";
        int currentNum = CompareCommandText(inputName);

        if (currentNum != 0 && !InGameManager.useInit)
        {
            GM.popPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = "Git init을 먼저 해주세요!!";
            GM.popPanel.SetActive(true);
            nameAll = "";
        }
        else if(correctCommand)
        {
            StateCheck(currentNum);
        }
        inputName = "";
    }

    private string nameAll;
    private bool correctCommand;
    public int CompareCommandText(string Text)
    {
        int currentNum = 0;
        correctCommand = true;

        if (Text.Contains("git init"))
        {
            currentNum = 0;
        }
        else if (Text.Contains("git log"))
        {
            currentNum = 1;
        }
        else if (Text.Contains("git status"))
        {
            currentNum = 2;
        }
        else if (Text.Contains("git add"))
        {
            nameAll = Text.Substring(8);
            currentNum = 3;
        }
        else if (Text.Contains("git commit -m"))
        {
            nameAll = Text.Substring(14);
            currentNum = 4;
        }
        else if (Text.Contains("create file"))
        {
            nameAll = Text.Substring(12);
            currentNum = 5;
        }
        else if (Text.Contains("git pull"))
        {
            currentNum = 6;
        }
        else if (Text.Contains("git branch -D"))
        {
            nameAll = Text.Substring(14);
            currentNum = 11;
        }
        else if (Text.Contains("git branch"))
        {
            Debug.Log(Text.Length);
            if (Text.Length-1 > 9)
            {
                nameAll = Text.Substring(11);
                currentNum = 8;
            }
            else
            {
                currentNum = 7;
            }
        }
        else if (Text.Contains("git checkout"))
        {
            currentNum = 9;
        }
        else if (Text.Contains("git merge"))
        {
            nameAll = Text.Substring(10);
            currentNum = 10;
        }
        else if (Text.Contains("git push"))
        {
            currentNum = 12;
        }
        else // 아무것도 포함되지 못했을때
        {
            GM.popPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = "올바르지 않은 명령어 입니다.";
            correctCommand = false;
            GM.popPanel.SetActive(true);
        }

        return currentNum;
    }
}
