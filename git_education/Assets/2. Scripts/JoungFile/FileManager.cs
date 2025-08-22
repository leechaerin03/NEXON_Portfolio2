using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;

// fileSample 구조체 생성 / 파일이름, 파일안내용, 상태로 구성
public class FileSample
{
    public string name;
    public string content;

    public enum EState
    {
        untracked,
        unmodified,
        modified,
        staged
    };
    public EState State;

    public FileSample(string name)
    {
        this.name = name;
        content = "11";
        State = EState.untracked;
    }

    public void InputContent(string txt)
    {
        content = txt;
    }

    public string returnContent()
    {
        return content;
    }
}

public class FileManager : MonoBehaviour
{
    public Dictionary<string, List<FileSample>> fileData = new Dictionary<string, List<FileSample>>();
    [SerializeField]
    GameObject fileSample;
    public TMP_InputField inputTextBox;
        
    public InGameManager GM;
    public ButtonManager BG;
    public MissionManager MM;
    public TreeMaker TM;
    public TMP_Text fileTitle;
    public TMP_Text fileName;

    public string currentFileBranch = "";

    private void Awake()
    {
        fileTitle = GameObject.Find("FileTitle").GetComponent<TMP_Text>();
        GM = GameObject.Find("GameManager").GetComponent<InGameManager>();
        BG = GameObject.Find("GameManager").GetComponent<ButtonManager>();
        MM = GameObject.Find("GameManager").GetComponent<MissionManager>();
        TM = GameObject.Find("treemaker").GetComponent<TreeMaker>();
    }

    private void Start()
    {

    }

    public void Update()
    {
        if(TM.click_obj && currentFileBranch != TM.click_obj.name)
        {
            GM.currentBranch = TM.click_obj.name;
            filePanelSetting();
        }
    }

    // fileData안에 값 넣어두기
    public void InputFileData(string fileName)
    {
        if (fileData.ContainsKey(GM.currentBranch))
        {
            Debug.Log("old file input : " + fileName);
            fileData[GM.currentBranch].Add(new FileSample(fileName));
        }
        else
        {
            Debug.Log("new file input : " + fileName);
            FileSample currentSample = new FileSample(fileName);
            fileData.Add(GM.currentBranch, new List<FileSample>());
            fileData[GM.currentBranch].Add(currentSample);
        }
    }

    // 파일 버튼을 눌렀을 때 초기 panel 설정
    public void filePanelSetting()
    {
        if (currentFileBranch == GM.currentBranch)
        {
            if (fileData.ContainsKey(currentFileBranch) && GM.filePanel.transform.childCount < fileData[currentFileBranch].Count)
            {
                if (InGameManager.useStatus)
                {
                    GameObject currentFile = Instantiate(fileSample, new Vector3(0, 0, 0), Quaternion.identity, GM.filePanel.transform);
                    currentFile.transform.GetChild(0).GetComponent<TMP_Text>().text = fileData[currentFileBranch][fileData[currentFileBranch].Count - 1].name + "-" + fileData[currentFileBranch][fileData[currentFileBranch].Count - 1].State;
                }
                else
                {
                    GameObject currentFile = Instantiate(fileSample, new Vector3(0, 0, 0), Quaternion.identity, GM.filePanel.transform);
                    currentFile.transform.GetChild(0).GetComponent<TMP_Text>().text = fileData[currentFileBranch][fileData[currentFileBranch].Count - 1].name;
                }
            }
        }

        else
        {
            foreach (Transform child in GM.filePanel.transform)
            {
                Destroy(child.gameObject);
            }

            currentFileBranch = GM.currentBranch;
            fileTitle.text = currentFileBranch;
            if (fileData.ContainsKey(currentFileBranch))
            {
                if (InGameManager.useStatus)
                {
                    for (int count = 0; count < fileData[currentFileBranch].Count; count++)
                    {
                        GameObject currentFile = Instantiate(fileSample, new Vector3(0, 0, 0), Quaternion.identity, GM.filePanel.transform);
                        currentFile.transform.GetChild(0).GetComponent<TMP_Text>().text = fileData[currentFileBranch][count].name + "-" + fileData[currentFileBranch][count].State;
                    }
                }
                else
                {
                    for (int count = 0; count < fileData[currentFileBranch].Count; count++)
                    {
                        GameObject currentFile = Instantiate(fileSample, new Vector3(0, 0, 0), Quaternion.identity, GM.filePanel.transform);
                        currentFile.transform.GetChild(0).GetComponent<TMP_Text>().text = fileData[currentFileBranch][count].name;
                    }
                }
            }
        }
    }

    public void FilePanelSettingPR()
    {
        foreach (Transform child in GM.filePanel.transform)
        {
            Destroy(child.gameObject);
        }

        currentFileBranch = GM.currentBranch;
        fileTitle.text = currentFileBranch;
        if (fileData.ContainsKey(currentFileBranch))
        {
            if (InGameManager.useStatus)
            {
                for (int count = 0; count < fileData[currentFileBranch].Count; count++)
                {
                    GameObject currentFile = Instantiate(fileSample, new Vector3(0, 0, 0), Quaternion.identity, GM.filePanel.transform);
                    currentFile.transform.GetChild(0).GetComponent<TMP_Text>().text = fileData[currentFileBranch][count].name + "-" + fileData[currentFileBranch][count].State;
                }
            }
            else
            {
                for (int count = 0; count < fileData[currentFileBranch].Count; count++)
                {
                    GameObject currentFile = Instantiate(fileSample, new Vector3(0, 0, 0), Quaternion.identity, GM.filePanel.transform);
                    currentFile.transform.GetChild(0).GetComponent<TMP_Text>().text = fileData[currentFileBranch][count].name;
                }
            }
        }
    }

    public void FilePanelSettingPR(string name)
    {
        foreach (Transform child in GM.filePanel.transform)
        {
            Destroy(child.gameObject);
        }

        GM.currentBranch = name;
        currentFileBranch = GM.currentBranch;
        fileTitle.text = currentFileBranch;
        if (fileData.ContainsKey(currentFileBranch))
        {
            if (InGameManager.useStatus)
            {
                for (int count = 0; count < fileData[currentFileBranch].Count; count++)
                {
                    GameObject currentFile = Instantiate(fileSample, new Vector3(0, 0, 0), Quaternion.identity, GM.filePanel.transform);
                    currentFile.transform.GetChild(0).GetComponent<TMP_Text>().text = fileData[currentFileBranch][count].name + "-" + fileData[currentFileBranch][count].State;
                }
            }
            else
            {
                for (int count = 0; count < fileData[currentFileBranch].Count; count++)
                {
                    GameObject currentFile = Instantiate(fileSample, new Vector3(0, 0, 0), Quaternion.identity, GM.filePanel.transform);
                    currentFile.transform.GetChild(0).GetComponent<TMP_Text>().text = fileData[currentFileBranch][count].name;
                }
            }
        }
    }

    public void PRPanelSetting()
    {
        foreach (Transform child in GM.prPanelContent.transform)
        {
            Destroy(child.gameObject);
        }

        if (fileData.ContainsKey("Master"))
        {
            if (InGameManager.useStatus)
            {
                for (int count = 0; count < fileData["Master"].Count; count++)
                {
                    GameObject currentFile = Instantiate(fileSample, new Vector3(0, 0, 0), Quaternion.identity, GM.prPanelContent.transform);
                    currentFile.transform.GetChild(0).GetComponent<TMP_Text>().text = fileData["Master"][count].name + "-" + fileData["Master"][count].State;
                }
            }
            else
            {
                for (int count = 0; count < fileData["Master"].Count; count++)
                {
                    GameObject currentFile = Instantiate(fileSample, new Vector3(0, 0, 0), Quaternion.identity, GM.prPanelContent.transform);
                    currentFile.transform.GetChild(0).GetComponent<TMP_Text>().text = fileData["Master"][count].name;
                }
            }
        }
    }

    // 파일 상태 확인용 함수
    public void setStatus()
    {
        if (MissionManager.currentMissionNumber == 1003 || MissionManager.currentMissionNumber == 1005 || MissionManager.currentMissionNumber == 2006)
        {
            MissionManager.currentMissionNumber += 1;
            MM.InputPanelData();
        }

        foreach (Transform child in GM.filePanel.transform)
        {
            Destroy(child.gameObject);
        }

        if (fileData.ContainsKey(currentFileBranch))
        {
            if (InGameManager.useStatus)
            {
                for (int count = 0; count < fileData[currentFileBranch].Count; count++)
                {
                    GameObject currentFile = Instantiate(fileSample, new Vector3(0, 0, 0), Quaternion.identity, GM.filePanel.transform);
                    currentFile.transform.GetChild(0).GetComponent<TMP_Text>().text = fileData[currentFileBranch][count].name + "-" + fileData[currentFileBranch][count].State;
                }
            }

            else
            {
                for (int count = 0; count < fileData[currentFileBranch].Count; count++)
                {
                    GameObject currentFile = Instantiate(fileSample, new Vector3(0, 0, 0), Quaternion.identity, GM.filePanel.transform);
                    currentFile.transform.GetChild(0).GetComponent<TMP_Text>().text = fileData[currentFileBranch][count].name;
                }
            }
        }
    }

    // 파일 저장 버튼을 눌렀을 때 사용하는 함수
    public void SaveFile()
    {
        if (MissionManager.currentMissionNumber == 4003 && fileName.text == "소감문")
        {
            MissionManager.currentMissionNumber += 1;
            MM.InputPanelData();
        }

        if (MissionManager.currentMissionNumber == 3002 && fileName.text == "park")
        {
            MissionManager.currentMissionNumber += 1;
            MM.InputPanelData();
        }

        int count;
        for (count = 0; count < fileData[currentFileBranch].Count; count++)
        {
            if(fileData[currentFileBranch][count].name == fileName.text)
            {
                if (fileData[currentFileBranch][count].content == inputTextBox.text)
                {
                    break;
                }

                else
                {
                    fileData[currentFileBranch][count].State = FileSample.EState.untracked;
                    fileData[currentFileBranch][count].InputContent(inputTextBox.text);
                    break;
                }
            }
        }

        BG.fileContentInput();
        inputTextBox.text = "";
    }
}
