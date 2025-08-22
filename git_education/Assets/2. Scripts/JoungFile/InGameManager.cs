using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class InGameManager : MonoBehaviour
{
    static public bool useInit = false;
    static public bool useStatus = false;

    // 사용자 inputData 게임오브젝트
    public TMP_InputField fileNameInput;
    // 사용자 tooltip 적어놓는 공간
    public RectTransform toolPanel;
    // 현재 사용 브런치
    public string currentBranch = "Master Branch";
    // userPanels
    public GameObject[] userInput = new GameObject[3];
    // PRPanel
    public RectTransform prPanel;
    public GameObject prPanelContent;

    // file Panel 오브젝트들
    public GameObject filePanel;
    public GameObject fileInputPanel;
    public FileManager FM;

    // pop 오브젝트들
    public GameObject popPanel;

    public InputManager IM;

    private void Awake()
    {
        //------------------------------ userPanel ------------------------------//
        userInput[0] = GameObject.Find("InGameUser");
        userInput[1] = userInput[0].transform.GetChild(0).gameObject;
        userInput[2] = userInput[0].transform.GetChild(1).gameObject;
        fileNameInput = userInput[2].transform.GetChild(1).GetComponent<TMP_InputField>();

        //------------------------------ toolPanel ------------------------------//
        toolPanel = GameObject.Find("InGameToolTipBackGround").GetComponent<RectTransform>();

        //------------------------------ filePanel ------------------------------//
        filePanel = GameObject.Find("FileContent");
        fileInputPanel = GameObject.Find("InGameTextPanel");
        FM = GameObject.Find("GameManager").GetComponent<FileManager>();

        //------------------------------ popPanel -------------------------------//
        popPanel = GameObject.Find("InGamePop").transform.GetChild(0).gameObject;

        //------------------------------ PRPanel --------------------------------//
        prPanel = GameObject.Find("InGamePRBackGround").GetComponent<RectTransform>();
        prPanelContent = GameObject.Find("PRContent");

        IM = GameObject.Find("GameManager").GetComponent<InputManager>();   
        useInit = false;
        useStatus = false;
    }

    private void Update()
    {
        RayHitObj();
    }

    public void TipPanelOpen()
    {
        // tooltip 내려오고 올라오고 그렇습니다! DOTween 사용쓰
        if (toolPanel.anchoredPosition.y > 0)
            toolPanel.DOAnchorPosY(-330, 1f).SetEase(Ease.OutBounce);
        else
            toolPanel.DOAnchorPosY(330, 0.5f).SetEase(Ease.Linear);
    }

    public void PRPanelOpen()
    {
        if (prPanel.anchoredPosition.x > 0)
            prPanel.DOAnchorPosX(-225, 0.5f).SetEase(Ease.Linear);
        else
            prPanel.DOAnchorPosX(225, 0.5f).SetEase(Ease.OutBounce);
    }

    void RayHitObj()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 MousePos = Input.mousePosition;
            MousePos = Camera.main.ScreenToWorldPoint(MousePos);

            RaycastHit2D hit = Physics2D.Raycast(MousePos, transform.forward, 15f);
            if (hit)
            {
                Debug.Log(hit.transform.name);
                currentBranch = hit.transform.name;
                FM.filePanelSetting();
            }
        }
    }
}
