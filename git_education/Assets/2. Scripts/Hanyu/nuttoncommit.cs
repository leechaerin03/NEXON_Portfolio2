using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//Ŀ�� ��ư 
public class nuttoncommit : MonoBehaviour
{
    public TreeMaker TM;
    public line ll,cl,clp;
    public TMP_InputField namezz;
    // Start is called before the first frame update
    void Start()
    {
        TM = GameObject.Find("treemaker").GetComponent<TreeMaker>(); //���õ� ��� ã���
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
        cl = TM.click_obj.GetComponent<line>(); //Ŭ���� �ν��Ͻ��� line script
        clp = cl.parents.GetComponent<line>(); //�θ�귣ġ �ν��Ͻ� ������
        int stnum = 0;
        if(FM.fileData.ContainsKey(clp.reef.name))
            for (int i = 0; i < FM.fileData[clp.reef.name].Count; i++)
                if (FM.fileData[clp.reef.name][i].State == FileSample.EState.staged)
                    stnum++;

            if (stnum == 0)
            {
                GM.popPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = "�ƹ��͵� Ŀ�Ե��� �ʾҽ��ϴ�.";
                GM.popPanel.SetActive(true);
                return;
            }

            GameObject myInstance = Instantiate(prefab); //���� ���
        myInstance.name = name;
        ll = myInstance.GetComponent<line>(); //������ �ν��Ͻ��� line script
        
        ll.parents = cl.parents; //���� ����� �θ�� Ŭ���� ����� �θ�� ����(���� �귣ġ)
        
        ll.connectedProduct = clp.reef; //������ ���� �θ�귣ġ�� ������ ��������

        //���� ���� 
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
 
        clp.reef = myInstance; //�θ�귣ġ�� ���� �ʱ�ȭ
    }
}
