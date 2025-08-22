using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;

    void Start()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    void GenerateData() 
    {
        talkData.Add(1, new string[] { "���� �Ի� ù���̴�...!", "�� �ٵ� �� �ؾ�����?", "�ϴ� ����Բ� ������!" });
        //��ȭ ������
        talkData.Add(1000, new string[] { "�ڳװ� �̹��� �Ի��� ģ������?", "�� ���� �� �ϰ� ������ ���� �Ŵ°ǰ�?","� ���� ���ϰ� !" });
        talkData.Add(2000, new string[] { "������ �� ���� ����...����Բ� ������" });

        //����Ʈ ��ȭ ������
        talkData.Add(10 + 1000, new string[] {"��...�׷� �ڳ��� ù ������ �־����" , "���� �� �и� Git�� ���� �� �ȴٰ� �߾���?", "��ħ �츮 ȸ�簡 ���ο� ������Ʈ�� �����Ϸ��� �ϴµ�", "git���� �۾��� ����Ҷ� ���� �������Գ�" });
        talkData.Add(11 + 2000, new string[] { "��...ū�ϳ���...", "���� �� �ڽ� �ְ� �� �� �ȴٰ� ������ �� Git�� ���� �ƹ��͵� �𸣴µ�...", "�и� ��Ű�� �ذ���Ұ� �и���....","����Կ��� ��Ű�� �ʵ��� Git�� �������� !" });

        talkData.Add(20 + 1000, new string[] { "��ȣ~ ���� ����� ���� ���� ���ϳ�~", "��..�ٵ� �츮 ȸ�翣 ������ ������ ������...", "���� ������ �۾��ϸ� �ȵ��� �ʳ�?", "���� �ٸ� ������ �۾��ϰ� �ѹ��� ��ĥ �� �ִ� ��� �� �˾ƿ��ְԳ�" });
        talkData.Add(21 + 2000, new string[] { "��... ù�ӹ��� �ܿ� �Ѱ��", "Git ���� �Ҹ��Ѱ�?!", "�ٷ� ���� ������ �����غ���!", "��... ��! Branch�� �̿��ϸ� �ǰڴµ�?" });

        talkData.Add(30 + 1000, new string[] { "�ڳ� ���󿡼� ���� �߰ſ� ������ ���� �ƴ°�?", "�ٷ� õ�������� ! ���� �����?", "�� ����� �ٵ� ���� ���̾� �� �μ� �ں����̶� ���� ������ �ߴµ�...", "merge�� �ȵ�... �ں��� ���δ� �� ��salary���ܾ� ���ʸ� ���ƴٴµ� ������ ����?", "�ͱ�? ���� �˾ƿ��Գ�" });
        talkData.Add(31 + 2000, new string[] { "���� ����� ����...��̾�� ȥ����", "���� ��¦ ������ ���� ���� �غ���!" });

        talkData.Add(40 + 1000, new string[] { "�� �׷��׷� �װ� ��������...", "���� ������ �������...", "���� ���� �� �������� ��������ҿ� Push�ϰ� �̰� �ڳ� ��ǻ�Ϳ� Pull�ϴ� �۾� ���ְ�", "���� ���ı�����!" });
        talkData.Add(41 + 2000, new string[] { "�������̶� �׷���...��Ƴ�", "�� �ٽ� �ǻ��ܺ��� ������� �ֽ� ������ ��������ҿ� push�ϰ�","�̰� �� ��ǻ�� �귱ġ���� reaf��忡 pull�ϸ� �ǰ���?","�غ���!" });

        talkData.Add(50 + 1000, new string[] { "�ڳ�! ���� GIT�� ���ϴ±���", "�ڳ׸� �̱� ���߾�! ����", "�����ε� �̷��� �����ְԳ� ���� �ڳ״� �������̾� ������!", "�����ε� �̷��� �����ְ� ������!!" });
        talkData.Add(51 + 2000, new string[] { "���� ���� �����̶��...", "�����ε� ������ GIT�� ��������!" });
    }

    public string GetTalk(int id, int talkIndex) 
    {
        if (!talkData.ContainsKey(id))
        {
            if (!talkData.ContainsKey(id - id % 10))
            {
                //�⺻ ��� ����ó��
                if (talkIndex == talkData[id - id % 100].Length)
                    return null;
                else
                    return talkData[id - id % 100][talkIndex];
            }
            else 
            {
                //����Ʈ ������ ��簡 ������, ����Ʈ �� ó�� ��� �������
                if (talkIndex == talkData[id - id % 10].Length)
                    return null;
                else
                    return talkData[id - id % 10][talkIndex];
            }
        }

        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }

    public string StartGetTalk(int talkIndex) 
    {
        if (talkIndex == talkData[1].Length)
            return null;
        else
            return talkData[1][talkIndex];
    }



}
