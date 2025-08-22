using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//�귣ġ ��ư
public class nutton : MonoBehaviour
{
    public TreeMaker TM;
    private line ll;
    public TMP_InputField namezz;
    // Start is called before the first frame update
    void Awake()
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
        GameObject myInstance = Instantiate(prefab); //���� ���
        myInstance.name = name;
        ll = myInstance.GetComponent<line>(); //������ �ν��Ͻ��� line script

        ll.parents = myInstance;
        ll.reef = myInstance;
        //ll.comName = namezz.text; //��� �̸� �����ֱ�
        //�ʱ� �귣ġ�� �ڱ� �ڽ��� �θ����� ����
        if (TM.click_obj)
        {
            FM = GameObject.Find("GameManager").GetComponent<FileManager>();
            ll.connectedProduct = TM.click_obj; //���� �ν���Ʈ�� Ŭ�� �ν���Ʈ ����       
            if (FM.fileData.ContainsKey(TM.click_obj.name))
            {
                FM.fileData.Add(myInstance.name, new List<FileSample>());
                int indexCount = 0;
                for (int i = 0; i < FM.fileData[TM.click_obj.name].Count; i++)
                {
                    if (FM.fileData[TM.click_obj.name][i].State == FileSample.EState.staged)
                    {
                        FM.fileData[myInstance.name].Add(new FileSample(FM.fileData[TM.click_obj.name][i].name));
                        FM.fileData[myInstance.name][indexCount].content = FM.fileData[TM.click_obj.name][i].content;
                        FM.fileData[myInstance.name][indexCount].State = FileSample.EState.staged;
                        indexCount++;
                    }
                }
            }

        }
        else
        {
            ll.connectedProduct = myInstance;
            TM.click_obj = myInstance;
            TM.click_obj.transform.localScale *= 1.3f;
        }

    }
}
