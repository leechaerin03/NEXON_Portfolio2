using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//커밋 버튼 
public class nuttoncommit : MonoBehaviour
{
    public TreeMaker TM;
    public line ll,cl,clp;
    public TMP_InputField namezz;
    // Start is called before the first frame update
    void Start()
    {
        TM = GameObject.Find("treemaker").GetComponent<TreeMaker>(); //선택된 노드 찾기용
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public GameObject prefab;
    public InGameManager GM;
    public FileManager FM;
    public void buttons(string name)
    {
        TM = GameObject.Find("treemaker").GetComponent<TreeMaker>();
        GM = GameObject.Find("GameManager").GetComponent<InGameManager>();
        FM = GameObject.Find("GameManager").GetComponent<FileManager>();
        cl = TM.click_obj.GetComponent<line>(); //클릭된 인스턴스의 line script
        clp = cl.parents.GetComponent<line>(); //부모브랜치 인스턴스 가져옴
        int stnum = 0;
        if(FM.fileData.ContainsKey(clp.reef.name))
            for (int i = 0; i < FM.fileData[clp.reef.name].Count; i++)
                if (FM.fileData[clp.reef.name][i].State == FileSample.EState.staged)
                    stnum++;

            if (stnum == 0)
            {
                GM.popPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = "아무것도 커밋되지 않았습니다.";
                GM.popPanel.SetActive(true);
                return;
            }

            GameObject myInstance = Instantiate(prefab); //생성 노드
        myInstance.name = name;
        ll = myInstance.GetComponent<line>(); //생성된 인스턴스의 line script
        
        ll.parents = cl.parents; //생성 노드의 부모는 클릭된 노드의 부모와 같음(같은 브랜치)
        
        ll.connectedProduct = clp.reef; //생성된 노드와 부모브랜치의 리프와 연결해줌

        //파일 복사 
        FM.fileData.Add(myInstance.name, new List<FileSample>());
        if (FM.fileData.ContainsKey(clp.reef.name))
        {
            int indexCount = 0;
            for (int i = 0; i < FM.fileData[clp.reef.name].Count; i++)
            {
                if(FM.fileData[clp.reef.name][i].State == FileSample.EState.staged)
                {
                    FM.fileData[myInstance.name].Add(new FileSample(FM.fileData[clp.reef.name][i].name));
                    FM.fileData[myInstance.name][indexCount].content = FM.fileData[clp.reef.name][i].content;
                    FM.fileData[myInstance.name][indexCount].State = FileSample.EState.staged;
                    indexCount++;
                }
            }
        }
 
        clp.reef = myInstance; //부모브랜치의 리프 초기화
    }
}
