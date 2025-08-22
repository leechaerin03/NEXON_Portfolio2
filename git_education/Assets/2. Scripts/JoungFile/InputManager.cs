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

    // string = ��ɾ�, enum = ���� int�� ����
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

    // git ��ɾ� ��� ���� 0 ~ N
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
        Debug.Log("" + PlayerPrefs.GetInt("����Ʈ")+ " : " +PlayerPrefs.GetInt("Mode"));
        if (PlayerPrefs.GetInt("Mode") == 1)
        {
            GM.userInput[2].transform.GetChild(0).GetComponent<TMP_Text>().text = "��ɾ �Է����ּ���";
            GM.userInput[1].SetActive(false);
            GM.userInput[2].SetActive(true);
        }
        MakeSetButton();

        if (PlayerPrefs.GetInt("����Ʈ") == 20)
        {
            if (PlayerPrefs.GetInt("Mode") == 1)
            {
                StateCheck(0); // git init ����
                GM.currentBranch = "Master Branch";
                StateCheck(CompareCommandText("create file park"));
                FM.fileData[GM.currentBranch][0].State = FileSample.EState.staged;
                StateCheck(CompareCommandText("git commit -m Version1"));
            }

            else if (PlayerPrefs.GetInt("Mode") == 0)
            {
                PlayerPrefs.SetInt("Mode", 1);
                StateCheck(0); // git init ����
                GM.currentBranch = "Master Branch";
                StateCheck(CompareCommandText("create file park"));
                FM.fileData[GM.currentBranch][0].State = FileSample.EState.staged;
                StateCheck(CompareCommandText("git commit -m Version1"));
                PlayerPrefs.SetInt("Mode", 0);
            }
            //InGameManager.useInit = true;
        }

        if (PlayerPrefs.GetInt("����Ʈ") == 30)
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
                FM.fileData[GM.currentBranch][FM.fileData[GM.currentBranch].Count - 1].content = "�츮���� 1���� ���̴� �� �׷�����";
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
                FM.fileData[GM.currentBranch][FM.fileData[GM.currentBranch].Count - 1].content = "�츮���� 1���� ���̴� �� �׷�����";
                FM.fileData[GM.currentBranch][FM.fileData[GM.currentBranch].Count - 1].State = FileSample.EState.staged;
                StateCheck(CompareCommandText("git branch"));
                PlayerPrefs.SetInt("Mode", 0);
            }
        }

        if (PlayerPrefs.GetInt("����Ʈ") == 40)
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
                StateCheck(CompareCommandText("create file �Ұ���"));
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
                StateCheck(CompareCommandText("create file �Ұ���"));
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
    // �귱ġ�ȿ� ���� �����ϴ� �Լ�
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

    // �귱ġ���� ��������ҷ� Ŀ���� �� �̸� �ִ� �Լ�
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

        inputName = ""; // �̸� �ʱ�ȭ
        GM.userInput[2].SetActive(false);
        GM.userInput[1].SetActive(true);
    }

    // �귱ġ �����Ҷ� �̸� �����ϴ� �Լ�
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
        inputName = ""; //�̸� �ʱ�ȭ
        TM.branmode = false;
        GM.userInput[2].SetActive(false);
        GM.userInput[1].SetActive(true);
    }

    // ���ϴ� ������ Ŀ�Խ�ų�� �Է��ϴ� �Լ�
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

                if (MissionManager.currentMissionNumber == 4004 && inputName == "�Ұ���")
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
            GM.popPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = inputName + " - �ش� ������ ã�� �� �����ϴ�.";
            GM.popPanel.SetActive(true);
            GM.userInput[2].SetActive(false);
            GM.userInput[1].SetActive(true);
        }

        inputName = ""; //�̸� �ʱ�ȭ
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

    // ����� ��ǲ ��ư ����
    public void MakeSetButton()
    {
        for(int i = 0; i < userButtons.Length; i++)
        {
            Instantiate(userButtons[i], new Vector3(0, 0, 0), Quaternion.identity, GM.userInput[1].transform);
        }
    }

    // ��ǲ ��ư�� �� �ƹ��ų� ������ �� �����ϴ� ���� �Լ�
    public void ChlickButton()
    {
        GameObject currentButton = EventSystem.current.currentSelectedGameObject;
        GM = GameObject.Find("GameManager").GetComponent<InGameManager>(); // ���Ӱ� ������� ��ư���ٰ� �������
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
                GM.popPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = "Git init�� ���� ���ּ���!!";
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
                    GM.popPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = "����Ұ� �̹� �����մϴ�.";
                }
                else
                {
                    bb = GameObject.Find("treemaker").GetComponent<nutton>();
                    bb.buttons("Master Branch");
                    GM.popPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = "���ο� ����Ҹ� �����մϴ�.";
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
                    GM.userInput[2].transform.GetChild(0).GetComponent<TMP_Text>().text = "���� �귱ġ�� �߰��� ���� �̸��� �����ּ���";
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

                            if (MissionManager.currentMissionNumber == 4004 && nameAll == "�Ұ���")
                            {
                                MissionManager.currentMissionNumber += 1;
                                MM.InputPanelData();
                            }
                            break;
                        }
                    }

                    if (count == FM.fileData[GM.currentBranch].Count)
                    {
                        GM.popPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = inputName + " - �ش� ������ ã�� �� �����ϴ�.";
                        GM.popPanel.SetActive(true);
                    }

                    nameAll = ""; //�̸� �ʱ�ȭ
                }

                else
                {
                    GM.popPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = "���� �귱ġ�� ������ �������� �ʽ��ϴ�.";
                    GM.popPanel.SetActive(true);
                }
                break;

            case EInputState.Commit:
                if (!TM.click_obj)
                {
                    GM.popPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = "�귱ġ�� �������� �ʽ��ϴ�.";
                    GM.popPanel.SetActive(true);
                    break;
                }
                currentInputData = (int)EInputState.Commit;
                if (PlayerPrefs.GetInt("Mode") == 0)
                {
                    GM.userInput[2].transform.GetChild(0).GetComponent<TMP_Text>().text = "���� �̸��� �Է����ּ���";
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
                    GM.userInput[2].transform.GetChild(0).GetComponent<TMP_Text>().text = "���� �̸��� �Է����ּ���";
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
                    GM.popPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = "���� �������丮�� ������ ���������ʽ��ϴ�.";
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
                    GM.userInput[2].transform.GetChild(0).GetComponent<TMP_Text>().text = "�귣ġ �̸��� �Է����ּ���";
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
                else if(PlayerPrefs.GetInt("Mode") == 1) // Text ���� merge
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
                            GM.popPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = "���� ������ merge�� �� �����ϴ�. park ������ salary���� Ȯ�����ּ���";
                            GM.popPanel.SetActive(true);
                        }
                    }
                    else
                    {
                        GM.popPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = "�귱ġ�� ���������ʽ��ϴ�.";
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
                else // Text ���� branchDelete
                {
                    if (FM.fileData.ContainsKey(nameAll)) // branch ���� �κ�
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
                        GM.popPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = "�귱ġ�� ���������ʽ��ϴ�.";
                        GM.popPanel.SetActive(true);
                    }
                }
                break;

            case EInputState.Push:
                for(count = 0; count < FM.fileData[GM.currentBranch].Count; ++count)
                {
                    if(FM.fileData[GM.currentBranch][count].State != FileSample.EState.staged)
                    {
                        GM.popPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = "commit�� ���� ���ּ���!";
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

                    if (FM.fileData.ContainsKey("Master")) // �̹� �ѹ� push�� �� �����϶�
                    {
                        // ���� �Ѱ��� ���ϸ鼭 �̹� �ִ� �������� �ƴ��� Ȯ�� �� ��ǲ
                        for(int fromCount = 0; fromCount < FM.fileData[GM.currentBranch].Count; fromCount++)
                        {
                            for (toCount = 0; toCount < FM.fileData["Master"].Count; ++toCount)
                            {
                                if(FM.fileData["Master"][toCount].name == FM.fileData[GM.currentBranch][fromCount].name) // ���� ������ ���� ���
                                {
                                    FM.fileData["Master"][toCount].content = FM.fileData[GM.currentBranch][fromCount].content; // ���� ���� ��� merge
                                }
                            }

                            if(toCount == FM.fileData["Master"].Count) // Master PR �ȿ� push�� ������ ���� ���
                            {
                                FileSample tmpFile = new FileSample(FM.fileData[GM.currentBranch][fromCount].name);
                                tmpFile.content = FM.fileData[GM.currentBranch][fromCount].content;
                                tmpFile.State = FileSample.EState.staged;
                                FM.fileData["Master"].Add(tmpFile);
                            }
                        }
                    }
                    else // PR �ȿ� �ƹ��͵� ���� ���� ���� ��� git push -u origin Master �� ���� ����
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
            GM.popPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = "Git init�� ���� ���ּ���!!";
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
        else // �ƹ��͵� ���Ե��� ��������
        {
            GM.popPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = "�ùٸ��� ���� ��ɾ� �Դϴ�.";
            correctCommand = false;
            GM.popPanel.SetActive(true);
        }

        return currentNum;
    }
}
