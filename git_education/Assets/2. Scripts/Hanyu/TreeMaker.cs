using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Burst.CompilerServices;

public class TreeMaker : MonoBehaviour
{
	public bool logmode = false;
	public bool branmode = false;
	public bool mergmode = false;
	bool dragon = false;
	public GameObject click_obj;
	public string nodename = "???";
	public FileManager FM;
	public InputManager IM;
	public InGameManager GM;
	public MissionManager MM;

	void Start()
    {
		click_obj = null;
	}
	public GameObject mergtmp,cltmp,checkoutname;
	public line cl,ml;
	void Update()
    {
		//���콺 Ŭ����
		if (Input.GetMouseButtonDown(0))
		{
			
			Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);
			if (hit.collider != null)
			{
				
				if(click_obj)
                {
					click_obj.transform.localScale /= 1.3f;
					mergtmp = click_obj;
				}

				click_obj = hit.transform.gameObject;
				/*GameObject checkM=Instantiate(checkoutname, GameObject.Find("Canvas").transform);
				checkM.GetComponent<textmaker>().text = checkM.transform.GetComponent<TMP_Text>();
				checkM.GetComponent<textmaker>().text.text = nodename;
				checkM.GetComponent<textmaker>().nodename = click_obj.name;
				checkM.transform.position = Input.mousePosition;
				*/
				Debug.Log("CheckOut" + click_obj.name);
				dragon = true;
				click_obj.transform.localScale *= 1.3f;
				FM = GameObject.Find("GameManager").GetComponent<FileManager>();
				if (FM.fileData.ContainsKey(click_obj.name) && FM.fileData.ContainsKey(mergtmp.name) && mergmode)
                {
					if (IM.CanMerge(mergtmp.name, click_obj.name))
					{
						if (MissionManager.currentMissionNumber == 2008 || MissionManager.currentMissionNumber == 3002)
						{
							MissionManager.currentMissionNumber += 1;
							MM.InputPanelData();
						}

						if (mergmode)
						{
							cl = click_obj.GetComponent<line>();
							cltmp = cl.parents;
							cl = cltmp.GetComponent<line>();
							cltmp = cl.reef;
							cl = cltmp.GetComponent<line>();  //reef ã�� ����

							ml = mergtmp.GetComponent<line>();
							mergtmp = ml.parents;
							ml = mergtmp.GetComponent<line>();
							mergtmp = ml.reef;
							ml = mergtmp.GetComponent<line>();

							FM = GameObject.Find("GameManager").GetComponent<FileManager>();

							//FM.fileData.Add(mergtmp.name, new List<FileSample>());
							if (FM.fileData.ContainsKey(cltmp.name))   //mergtmp�� cltmp�� �־����
							{
								int indexCount = FM.fileData[mergtmp.name].Count; //ī��Ʈ �̾�ޱ�
								for (int i = 0; i < FM.fileData[cltmp.name].Count; i++)
								{
									if (FM.fileData[cltmp.name][i].State == FileSample.EState.staged)  // ���� ���� ������������ �˻���
									{
										int check = -1;
										for (int j = 0; j < FM.fileData[mergtmp.name].Count; j++) //���� ���� ���� �ִ���
										{
											if (FM.fileData[mergtmp.name][j].name == FM.fileData[cltmp.name][i].name)
												check = j;
										}
										if (check == -1)  //���� ���ϳ��� ������
										{
											FM.fileData[mergtmp.name].Add(new FileSample(FM.fileData[cltmp.name][i].name));//���� �߰�
											FM.fileData[mergtmp.name][indexCount].content = FM.fileData[cltmp.name][i].content;
											FM.fileData[mergtmp.name][indexCount].State = FileSample.EState.staged;
											indexCount++;
										}
										else //d������ ����
										{
											FM.fileData[mergtmp.name][check].content = FM.fileData[cltmp.name][i].content;
											FM.fileData[mergtmp.name][check].State = FileSample.EState.staged;
										}

									}
								}
							}

							//ml.comName = cl.comName;
							mergmode = false;
						}
					}

					else
					{
						if (MissionManager.currentMissionNumber == 3001)
						{
							MissionManager.currentMissionNumber += 1;
							MM.InputPanelData();
						}
						GM.popPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = "���� ������ merge�� �� �����ϴ�. salary �ڿ� ���� Ȯ�����ּ���";
						GM.popPanel.SetActive(true);
						mergmode = false;	
					}
				}
			}
			
		}
		if(dragon)
        {
			click_obj.transform.position=Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,- Camera.main.transform.position.z));
		}
		if(Input.GetMouseButtonUp(0))
        {
			dragon = false;
		}
	}

	public void Merge(string name)
    {

        if (click_obj)
        {
            click_obj.transform.localScale /= 1.3f;
			mergtmp = click_obj;
		}

		click_obj = GameObject.Find(name);
        click_obj.transform.localScale *= 1.3f;
        if (mergmode)
        {
            cl = click_obj.GetComponent<line>();
            cltmp = cl.parents;
            cl = cltmp.GetComponent<line>();
            cltmp = cl.reef;
            cl = cltmp.GetComponent<line>();  //reef ã�� ����

            ml = mergtmp.GetComponent<line>();
            mergtmp = ml.parents;
            ml = mergtmp.GetComponent<line>();
            mergtmp = ml.reef;
            ml = mergtmp.GetComponent<line>();

            FM = GameObject.Find("GameManager").GetComponent<FileManager>();

            //FM.fileData.Add(mergtmp.name, new List<FileSample>());
            if (FM.fileData.ContainsKey(cltmp.name))   //mergtmp�� cltmp�� �־����
            {
                int indexCount = FM.fileData[mergtmp.name].Count; //ī��Ʈ �̾�ޱ�
                for (int i = 0; i < FM.fileData[cltmp.name].Count; i++)
                {
                    if (FM.fileData[cltmp.name][i].State == FileSample.EState.staged)  // ���� ���� ������������ �˻���
                    {
                        int check = -1;
                        for (int j = 0; j < FM.fileData[mergtmp.name].Count; j++) //���� ���� ���� �ִ���
                        {
                            if (FM.fileData[mergtmp.name][j].name == FM.fileData[cltmp.name][i].name)
                                check = j;
                        }
                        if (check == -1)  //���� ���ϳ��� ������
                        {
                            FM.fileData[mergtmp.name].Add(new FileSample(FM.fileData[cltmp.name][i].name));//���� �߰�
                            FM.fileData[mergtmp.name][indexCount].content = FM.fileData[cltmp.name][i].content;
                            FM.fileData[mergtmp.name][indexCount].State = FileSample.EState.staged;
                            indexCount++;
                        }
                        else //d������ ����
                        {
                            FM.fileData[mergtmp.name][check].content = FM.fileData[cltmp.name][i].content;
                            FM.fileData[mergtmp.name][check].State = FileSample.EState.staged;
                        }

                    }
                }
            }

            //ml.comName = cl.comName;
            mergmode = false;
        }
    }
}
